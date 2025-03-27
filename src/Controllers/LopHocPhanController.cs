using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
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
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? hocPhanId, [FromQuery] int? hocKyId, [FromQuery] int? giangVienId, [FromQuery] int? sinhVienId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var lopHocPhans = await _lopHocPhanService.GetFilteredLopHocPhansAsync(hocPhanId, hocKyId, giangVienId, sinhVienId, pageNumber, pageSize);
            var lopHocPhanDTOs = lopHocPhans.Select(lhp => lhp.ToLopHocPhanDTO()).ToList();
            return Ok(lopHocPhanDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var lopHocPhan = await _lopHocPhanService.GetLopHocPhanByIdAsync(id);
            if (lopHocPhan == null)
                return NotFound("Không tìm thấy Lớp học phần");
            var lopHocPhanDTO = lopHocPhan.ToLopHocPhanDTO();
            return Ok(lopHocPhanDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> Create([FromBody] CreateLopHocPhanDTO createLopHocPhanDTO)
        {
            try
            {
                var lopHocPhan = await _lopHocPhanService.CreateLopHocPhanAsync(createLopHocPhanDTO);
                var lopHocPhanDTO = lopHocPhan?.ToLopHocPhanDTO();
                return CreatedAtAction(nameof(GetById), new { id = lopHocPhan?.Id }, lopHocPhanDTO);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLopHocPhanDTO updateLopHocPhanDTO)
        {
            try
            {
                var lopHocPhan = await _lopHocPhanService.UpdateLopHocPhanAsync(id, updateLopHocPhanDTO);
                return Ok(lopHocPhan?.ToLopHocPhanDTO());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await _lopHocPhanService.DeleteLopHocPhanAsync(id);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        [HttpGet("{id}/sinhvien")]
        [Authorize]
        public async Task<IActionResult> GetSinhViens([FromRoute] int id)
        {
            try
            {
                var sinhViens = await _lopHocPhanService.GetSinhViensInLopHocPhanAsync(id);
                return Ok(sinhViens);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/sinhvien")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> AddSinhViens([FromRoute] int id, [FromBody] int[] sinhVienIds)
        {
            try
            {
                var result = await _lopHocPhanService.AddSinhViensToLopHocPhanAsync(id, sinhVienIds);
                return CreatedAtAction(nameof(GetSinhViens), new { id }, result.Select(sv => sv.ToSinhVienDTO()).ToList());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/sinhvien")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> UpdateSinhViens([FromRoute] int id, [FromBody] int[] sinhVienIds)
        {
            try
            {
                var result = await _lopHocPhanService.UpdateSinhViensInLopHocPhanAsync(id, sinhVienIds);
                return Ok(result.Select(sv => sv.ToSinhVienDTO()).ToList());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}/sinhvien/{sinhVienId}")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> RemoveSinhVien([FromRoute] int id, [FromRoute] int sinhVienId)
        {
            try
            {
                var result = await _lopHocPhanService.RemoveSinhVienFromLopHocPhanAsync(id, sinhVienId);
                return Ok(result.Select(sv => sv.ToSinhVienDTO()).ToList());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPut("{id}/congthucdiem")]
        [Authorize(Roles = "Admin,PhongDaoTao")]
        public async Task<IActionResult> UpdateCongThucDiem([FromRoute] int id, [FromBody] List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs)
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
            var baiKiemTraDTOs = await _baiKiemTraService.UpdateCongThucDiem(id, createBaiKiemTraDTOs);
            if (baiKiemTraDTOs == null)
            {
                return BadRequest("Không thể tạo công thức điểm");
            }
            return Ok(baiKiemTraDTOs);
        }
        [HttpGet("{lopHocPhanId}/chitiet")]
        [Authorize]
        public async Task<IActionResult> GetChiTietLopHocPhan([FromRoute] int lopHocPhanId)
        {
            var lopHocPhanChiTietDTO = await _lopHocPhanService.GetChiTietLopHocPhanDTO(lopHocPhanId);
            if (lopHocPhanChiTietDTO == null)
            {
                return NotFound("Không tìm thấy lớp học phần");
            }
            return Ok(lopHocPhanChiTietDTO);
        }
        [HttpGet("{id}/sinhviennotinlhp")]
        [Authorize]
        public async Task<IActionResult> GetSinhViensNotInLopHocPhan([FromRoute] int id)
        {
            var sinhViens = await _lopHocPhanService.GetSinhViensNotInLopHocPhanDTO(id);
            return Ok(sinhViens);
        }
    }
}
