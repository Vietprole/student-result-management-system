using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Interfaces;
using StudentResultManagementSystem.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/cauhoi")]
    [ApiController]
    // [Authorize]
    public class CauHoiController : ControllerBase
    {
        private readonly ICauHoiService _cauHoiService;
        private readonly ICLOService _cLOService;
        public CauHoiController(ICauHoiService cauHoiService, ICLOService cLOService)
        {
            _cauHoiService = cauHoiService;
            _cLOService = cLOService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? baiKiemTraId)
        {
            List<CauHoiDTO> cauHoiDTOs;
            if (baiKiemTraId.HasValue)
            {
                cauHoiDTOs = await _cauHoiService.GetCauHoisByBaiKiemTraIdAsync(baiKiemTraId.Value);
            }
            else
            {
                cauHoiDTOs = await _cauHoiService.GetAllCauHoisAsync();
            }
            return Ok(cauHoiDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cauHoiDTO = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (cauHoiDTO == null)
                return NotFound("Không tìm thấy câu hỏi");
            return Ok(cauHoiDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Create([FromBody] CreateCauHoiDTO createCauHoiDTO)
        {
            var cauHoiDTO = await _cauHoiService.CreateCauHoiAsync(createCauHoiDTO);
            return CreatedAtAction(nameof(GetById), new { id = cauHoiDTO.Id }, cauHoiDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoiDTO = await _cauHoiService.UpdateCauHoiAsync(id, updateCauHoiDTO);
            if (cauHoiDTO == null)
                return NotFound("Không tìm thấy câu hỏi");
            return Ok(cauHoiDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _cauHoiService.DeleteCauHoiAsync(id);
            if (!result)
                return NotFound("Không tìm thấy câu hỏi");
            return NoContent();
        }

        [HttpGet("{id}/clo")]
        [Authorize]
        public async Task<IActionResult> GetCLOs([FromRoute] int id)
        {
            var cauHoi = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            var cLODTOs = await _cLOService.GetCLOsByCauHoiIdAsync(id);
            return Ok(cLODTOs);
        }

        // [HttpPost("{id}/clo")]
        // public async Task<IActionResult> AddCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        // {
        //     var cauHoi = await _cauHoiService.GetCauHoiByIdAsync(id);
        //     if (cauHoi == null)
        //         return NotFound("CauHoi not found");

        //     var result = await _cauHoiService.AddCLOsToCauHoiAsync(id, cLOIds);

        //     if (!result.IsSuccess)
        //         return NotFound(result.ErrorMessage);

        //     return CreatedAtAction(nameof(GetCLOs), new { id = cauHoi.Id });
        // }

        [HttpPut("{id}/clo")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> UpdateCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        {
            var cauHoi = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            try
            {
                var updatedCLOs = await _cauHoiService.UpdateCLOsOfCauHoiAsync(id, cLOIds);
                return Ok(updatedCLOs);
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

        // [HttpDelete("{id}/clo/{cLOId}")]
        // public async Task<IActionResult> RemoveCLO([FromRoute] int id, [FromRoute] int cLOId)
        // {
        //     var result = await _cLOService.RemoveCLOFromCauHoiAsync(id, cLOId);
        //     if (!result)
        //         return NotFound("CauHoi or CLO not found");

        //     return NoContent();
        // }
    }
}
