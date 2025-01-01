using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.DiemDinhChinh;
using Student_Result_Management_System.Utils;
using StudentResultManagementSystem.Interfaces;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/diemdinhchinh")]
    [ApiController]
    public class DiemDinhChinhController : ControllerBase
    {
        private readonly IDiemDinhChinhService _diemDinhChinhService;

        public DiemDinhChinhController(IDiemDinhChinhService diemDinhChinhService)
        {
            _diemDinhChinhService = diemDinhChinhService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? lopHocPhanId, [FromQuery] int? giangVienId)
        {
            var diemDinhChinhs = await _diemDinhChinhService.GetDiemDinhChinhsAsync(lopHocPhanId, giangVienId);
            return Ok(diemDinhChinhs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var diemDinhChinh = await _diemDinhChinhService.GetDiemDinhChinhByIdAsync(id);
            if (diemDinhChinh == null)
                return NotFound("Không tìm thấy điểm đính chính");
            return Ok(diemDinhChinh);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Create([FromBody] CreateDiemDinhChinhDTO createDTO)
        {
            var result = await _diemDinhChinhService.CreateDiemDinhChinhAsync(createDTO);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDiemDinhChinhDTO updateDTO)
        {
            var result = await _diemDinhChinhService.UpdateDiemDinhChinhAsync(id, updateDTO);
            if (result == null)
                return NotFound("Không tìm thấy điểm đính chính");
            return Ok(result);
        }

        [HttpPut("upsert")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Upsert([FromBody] UpdateDiemDinhChinhDTO updateDTO)
        {
            try
            {
                var result = await _diemDinhChinhService.UpsertDiemDinhChinhAsync(updateDTO);
                return Ok(result);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,GiangVien")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _diemDinhChinhService.DeleteDiemDinhChinhAsync(id);
            if (!isDeleted)
                return NotFound("Không tìm thấy điểm đính chính");
            return NoContent();
        }

        [Authorize(Roles = "Admin,PhongDaoTao")]
        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var userIdInt = int.Parse(userId?? "0");
            try {
                var result = await _diemDinhChinhService.AcceptDiemDinhChinhAsync(id, userIdInt);
                return Ok(result);
            } catch (BusinessLogicException ex) {
                return BadRequest(ex.Message);
            } catch (NotFoundException ex) {
                return NotFound(ex.Message);
            }
        }
    }
}
