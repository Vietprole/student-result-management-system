using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Services;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/clo")]
    [ApiController]
    public class CLOController : ControllerBase
    {
        private readonly ICLOService _cloService;
        public CLOController(ICLOService cloService)
        {
            _cloService = cloService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? hocPhanId, [FromQuery] int? hocKyId)
        {
            var cLOs = await _cloService.GetFilteredCLOsAsync(hocPhanId, hocKyId);
            return Ok(cLOs.Select(c => c.ToCLODTO()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cLODTO = await _cloService.GetCLOByIdAsync(id);
            if (cLODTO == null)
                return NotFound("Không tìm thấy CLO");
            return Ok(cLODTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Create([FromBody] CreateCLODTO createCLODTO)
        {
            var cLODTO = await _cloService.CreateCLOAsync(createCLODTO);
            return CreatedAtAction(nameof(GetById), new { id = cLODTO.Id }, cLODTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCLODTO updateCLODTO)
        {
            var cLODTO = await _cloService.UpdateCLOAsync(id, updateCLODTO);
            if (cLODTO == null)
                return NotFound("Không tìm thấy CLO");
            return Ok(cLODTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,GiangVien")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _cloService.DeleteCLOAsync(id);
            if (!result)
                return NotFound("Không tìm thấy CLO");
            return NoContent();
        }
    }
}
