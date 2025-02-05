using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.DTOs.DiemDinhChinh;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Utils;

namespace ketQua_Result_Management_System.Controllers
{
    [Route("api/ketqua")]
    [ApiController]
    // [Authorize]
    public class KetQuaController : ControllerBase
    {
        private readonly IKetQuaService _ketQuaService;
        public KetQuaController(IKetQuaService ketQuaService)
        {
            _ketQuaService = ketQuaService;
        }

        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? baiKiemTraId, [FromQuery] int? sinhVienId, [FromQuery] int? lopHocPhanId) // async go with Task<> to make function asynchronous
        {
            var ketQuaDTOs = await _ketQuaService.GetFilteredKetQuasAsync(baiKiemTraId, sinhVienId, lopHocPhanId);
            return Ok(ketQuaDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var ketQuaDTO = await _ketQuaService.GetKetQuaByIdAsync(id);
            if (ketQuaDTO == null)
                return NotFound("Không tìm thấy kết quả");

            return Ok(ketQuaDTO);
        }

        [HttpPost]
        [Authorize(Roles="Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Create([FromBody] CreateKetQuaDTO createKetQuaDTO)
        {
            var ketQuaDTO = await _ketQuaService.CreateKetQuaAsync(createKetQuaDTO);
            return CreatedAtAction(nameof(GetById), new { id = ketQuaDTO.Id }, ketQuaDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles="Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateKetQuaDTO updateKetQuaDTO)
        {
            var updatedKetQuaDTO = await _ketQuaService.UpdateKetQuaAsync(id, updateKetQuaDTO);
            return Ok(updatedKetQuaDTO);
        }

        [HttpPut("upsert")]
        [Authorize(Roles="Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Upsert([FromBody] UpdateKetQuaDTO ketQuaDTO)
        {
            try
            {
                if (User.IsInRole("GiangVien"))
                {
                    var giangVienId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "giangVienId")?.Value ?? "0");
                    var isAllowed = _ketQuaService.AllowThisGiangVienToEdit(giangVienId, ketQuaDTO.SinhVienId, ketQuaDTO.CauHoiId);
                    if (!isAllowed)
                        return BadRequest("Giảng viên không dạy lớp này");
                }
                var result = await _ketQuaService.UpsertKetQuaAsync(ketQuaDTO);
                return Ok(result);
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

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _ketQuaService.DeleteKetQuaAsync(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin, GiangVien")]
        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmKetQuaDTO confirmKetQuaDTO)
        {
            try {
                if (User.IsInRole("GiangVien"))
                {
                    var giangVienId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "giangVienId")?.Value ?? "0");
                    var isAllowed = _ketQuaService.AllowThisGiangVienToConfirm(giangVienId, confirmKetQuaDTO.SinhVienId, confirmKetQuaDTO.CauHoiId);
                    if (!isAllowed)
                        return BadRequest("Giảng viên không dạy lớp này");
                }
                var ketQuaDTO = await _ketQuaService.ConfirmKetQuaAsync(confirmKetQuaDTO);
                return Ok(ketQuaDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, PhongDaoTao")]
        [HttpPost("accept")]
        public async Task<IActionResult> Accept([FromBody] AcceptKetQuaDTO acceptKetQuaDTO)
        {
            try {
                var isAllowed = _ketQuaService.AllowAccept(acceptKetQuaDTO);
                if (!isAllowed)
                    return BadRequest("Không thể duyệt kết quả này");
                var ketQuaDTO = await _ketQuaService.AcceptKetQuaAsync(acceptKetQuaDTO);
                return Ok(ketQuaDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("calculate-diem-clo")]
        [Authorize]
        public async Task<IActionResult> CalculateDiemCLO([FromQuery] int SinhVienId, [FromQuery] int cLOId, [FromQuery] bool useDiemTam = false)
        {
            try
            {
                var diemCLO = await _ketQuaService.CalculateDiemCLO(SinhVienId, cLOId, useDiemTam);
                return Ok(diemCLO);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("calculate-diem-clo-max")]
        [Authorize]
        public async Task<IActionResult> CalculateDiemCLOMax([FromQuery] int cLOId)
        {
            try {
                var diemCLOMax = await _ketQuaService.CalculateDiemCLOMax(cLOId);
                return Ok(diemCLOMax);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("calculate-diem-pk")]
        [Authorize]
        public async Task<IActionResult> CalculateDiemPk([FromQuery] int lopHocPhanId, [FromQuery] int sinhVienId, [FromQuery] int ploId, [FromQuery] bool useDiemTam = false)
        {
            try {
                var diemPk = await _ketQuaService.CalculateDiemPk(lopHocPhanId, sinhVienId, ploId, useDiemTam);
                return Ok(diemPk);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("calculate-diem-plo")]
        [Authorize]
        public async Task<IActionResult> CalculateDiemPLO([FromQuery] int sinhVienId, [FromQuery] int ploId, [FromQuery] bool useDiemTam = false)
        {
            try {
                var diemPLO = await _ketQuaService.CalculateDiemPLO(sinhVienId, ploId, useDiemTam);
                return Ok(diemPLO);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
    }
}
