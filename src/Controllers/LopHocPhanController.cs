using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace lopHocPhan_Result_Management_System.Controllers
{
    [Route("api/lophocphan")]
    [ApiController]
    // [Authorize]
    public class LopHocPhanController : ControllerBase
    {
        private readonly ILopHocPhanService _lopHocPhanService;
        private readonly IBaiKiemTraService _baiKiemTraService;
        public LopHocPhanController(ILopHocPhanService lopHocPhanService, IBaiKiemTraService baiKiemTraService)
        {
            _lopHocPhanService = lopHocPhanService;
            _baiKiemTraService = baiKiemTraService;
        }

        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll([FromQuery] int? hocPhanId, [FromQuery] int? hocKyId)
        {
            var lopHocPhans = await _lopHocPhanService.GetFilteredLopHocPhansAsync(hocPhanId, hocKyId);
            var lopHocPhanDTOs = lopHocPhans.Select(lhp => lhp.ToLopHocPhanDTO()).ToList();
            return Ok(lopHocPhanDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var lopHocPhan = await _lopHocPhanService.GetLopHocPhanByIdAsync(id);
            if (lopHocPhan == null)
                return NotFound("Không tìm thấy Lớp học phần");
            var lopHocPhanDTO = lopHocPhan.ToLopHocPhanDTO();
            return Ok(lopHocPhanDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLopHocPhanDTO createLopHocPhanDTO)
        {
            var lopHocPhan = await _lopHocPhanService.CreateLopHocPhanAsync(createLopHocPhanDTO);
            if (lopHocPhan == null)
            {
                return BadRequest("Không thể tạo lớp học phần mới.");
            }
            var lopHocPhanDTO = lopHocPhan.ToLopHocPhanDTO();
            return CreatedAtAction(nameof(GetById), new { id = lopHocPhan.Id }, lopHocPhanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLopHocPhanDTO updateLopHocPhanDTO)
        {
            try{
                var lopHocPhan = await _lopHocPhanService.UpdateLopHocPhanAsync(id, updateLopHocPhanDTO);
                return Ok(lopHocPhan?.ToLopHocPhanDTO());
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try {
                var result = await _lopHocPhanService.DeleteLopHocPhanAsync(id);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
            return NoContent();
        }

        [HttpGet("{id}/sinhvien")]
        public async Task<IActionResult> GetSinhViens([FromRoute] int id)
        {
            try {
                var sinhViens = await _lopHocPhanService.GetSinhViensInLopHocPhanAsync(id);
                return Ok(sinhViens);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/sinhvien")]
        public async Task<IActionResult> AddSinhViens([FromRoute] int id, [FromBody] int[] sinhVienIds)
        {
            try {
                var result = await _lopHocPhanService.AddSinhViensToLopHocPhanAsync(id, sinhVienIds);
                return CreatedAtAction(nameof(GetSinhViens), new { id }, result);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/sinhvien")]
        public async Task<IActionResult> UpdateSinhViens([FromRoute] int id, [FromBody] int[] sinhVienIds)
        {
            try {
                var result = await _lopHocPhanService.UpdateSinhViensInLopHocPhanAsync(id, sinhVienIds);
                return Ok(result);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}/sinhvien/{sinhVienId}")]
        public async Task<IActionResult> RemoveSinhVien([FromRoute] int id, [FromRoute] int sinhVienId)
        {
            try {
                var result = await _lopHocPhanService.RemoveSinhVienFromLopHocPhanAsync(id, sinhVienId);
                return Ok(result);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }
        [HttpPost("{id}/add-congthucdiem")]
        public async Task<IActionResult> AddCongThucDiem([FromRoute] int id, [FromBody] List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs)
        {
            if (!ModelState.IsValid)
            {
                // Lấy thông tin lỗi chi tiết
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Dữ liệu không hợp lệ", Errors = errors });
            }

            string check = await _lopHocPhanService.CheckCongThucDiem(createBaiKiemTraDTOs);
            if (check != "OK")
            {
                return BadRequest(check);
            }
            var congThucDiemDTO = await _baiKiemTraService.CreateCongThucDiem(id, createBaiKiemTraDTOs);
            if (congThucDiemDTO == null)
            {
                return BadRequest("Không thể tạo công thức điểm");
            }
            return Ok(congThucDiemDTO);
        }


        //[HttpGet("{id}/view-giangviens")]
        //public async Task<IActionResult> GetGiangViens([FromRoute] int id)
        //{
        //    var lopHocPhan = await _context.LopHocPhans
        //        .Include(lhp => lhp.GiangViens)
        //        .ThenInclude(gv => gv.TaiKhoan)
        //        .FirstOrDefaultAsync(lhp => lhp.Id == id);
        //    if (lopHocPhan == null)
        //        return NotFound();

        //    var giangVienDTOs = lopHocPhan.GiangViens.Select(sv => sv.ToGiangVienDTO()).ToList();
        //    return Ok(giangVienDTOs);
        //}


        //[HttpPost("{id}/add-giangviens")]
        //public async Task<IActionResult> AddGiangViens([FromRoute] int id, [FromBody] int[] giangVienIds)
        //{
        //    var lopHocPhan = await _context.LopHocPhans
        //        .Include(lhp => lhp.GiangViens) // Include GiangViens to ensure the collection is loaded
        //        .FirstOrDefaultAsync(lhp => lhp.Id == id);
        //    if (lopHocPhan == null)
        //        return NotFound("LopHocPhan not found");

        //    foreach (var giangVienId in giangVienIds)
        //    {
        //        var giangVien = await _context.GiangViens.FindAsync(giangVienId);
        //        if (giangVien == null)
        //            return NotFound($"GiangVien with ID {giangVienId} not found");

        //        // Check if the GiangVien is already in the LopHocPhan to avoid duplicates
        //        if (!lopHocPhan.GiangViens.Contains(giangVien))
        //        {
        //            lopHocPhan.GiangViens.Add(giangVien);
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetGiangViens), new { id = lopHocPhan.Id }, lopHocPhan.GiangViens.Select(sv => sv.ToGiangVienDTO()).ToList());
        //}

        //[HttpPut("{id}/update-giangviens")]
        //public async Task<IActionResult> UpdateGiangViens([FromRoute] int id, [FromBody] int[] giangVienIds)
        //{
        //    var lopHocPhan = await _context.LopHocPhans
        //        .Include(p => p.GiangViens)
        //        .FirstOrDefaultAsync(p => p.Id == id);
                
        //    if (lopHocPhan == null)
        //        return NotFound("Lop hoc phan not found");

        //    // Get existing GiangVien IDs
        //    var existingGiangVienIds = lopHocPhan.GiangViens.Select(c => c.Id).ToList();
            
        //    // Find IDs to add and remove
        //    var idsToAdd = giangVienIds.Except(existingGiangVienIds);
        //    var idsToRemove = existingGiangVienIds.Except(giangVienIds);

        //    // Remove GiangViens
        //    foreach (var removeId in idsToRemove)
        //    {
        //        var giangVienToRemove = lopHocPhan.GiangViens.First(c => c.Id == removeId);
        //        lopHocPhan.GiangViens.Remove(giangVienToRemove);
        //    }

        //    // Add new GiangViens
        //    foreach (var addId in idsToAdd)
        //    {
        //        var giangVien = await _context.GiangViens.FindAsync(addId);
        //        if (giangVien == null)
        //            return NotFound($"GiangVien with ID {addId} not found");
                    
        //        lopHocPhan.GiangViens.Add(giangVien);
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(lopHocPhan.GiangViens.Select(c => c.ToGiangVienDTO()).ToList());
        //}

        //[HttpDelete("{id}/remove-giangvien/{giangVienId}")]
        //public async Task<IActionResult> RemoveGiangVien([FromRoute] int id, [FromRoute] int giangVienId)
        //{
        //    var lopHocPhan = await _context.LopHocPhans
        //        .Include(lhp => lhp.GiangViens) // Include GiangViens to ensure the collection is loaded
        //        .FirstOrDefaultAsync(lhp => lhp.Id == id);
        //    if (lopHocPhan == null)
        //        return NotFound("LopHocPhan not found");

        //    var giangVien = await _context.GiangViens.FindAsync(giangVienId);
        //    if (giangVien == null)
        //        return NotFound($"GiangVien with ID {giangVienId} not found");

        //    // Check if the HocPhan is in the CTDT to avoid removing non-existing HocPhan
        //    if (!lopHocPhan.GiangViens.Contains(giangVien))
        //    {
        //        return NotFound($"GiangVien with ID {giangVienId} is not found in LopHocPhan with ID {id}");
        //    }
        //    lopHocPhan.GiangViens.Remove(giangVien);

        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetGiangViens), new { id = lopHocPhan.Id }, lopHocPhan.GiangViens.Select(sv => sv.ToGiangVienDTO()).ToList());
        //}
    }
}
