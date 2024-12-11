using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Services;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/clo")]
    [ApiController]
    [Authorize]
    public class CLOController : ControllerBase
    {
        private readonly CLOService _cloService;
        public CLOController(CLOService cloService)
        {
            _cloService = cloService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? lopHocPhanId)
        {
            if (lopHocPhanId.HasValue)
            {
                var cLODTOs1 = await _cloService.GetCLOsByLopHocPhanIdAsync(lopHocPhanId.Value);
                return Ok(cLODTOs1);
            }
            var cLODTOs = await _cloService.GetAllCLOsAsync();
            return Ok(cLODTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cLODTO = await _cloService.GetCLOByIdAsync(id);
            if (cLODTO == null)
                return NotFound("CLO not found");
            return Ok(cLODTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCLODTO createCLODTO)
        {
            var cLODTO = await _cloService.CreateCLOAsync(createCLODTO);
            return CreatedAtAction(nameof(GetById), new { id = cLODTO.Id }, cLODTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCLODTO updateCLODTO)
        {
            var cLODTO = await _cloService.UpdateCLOAsync(id, updateCLODTO);
            if (cLODTO == null)
                return NotFound("CLO not found");
            return Ok(cLODTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _cloService.DeleteCLOAsync(id);
            if (!result)
                return NotFound("CLO not found");
            return NoContent();
        }

    }
}
