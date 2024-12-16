using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/nganh")]
    [ApiController]
    // [Authorize]
    public class NganhController : ControllerBase
    {
        private readonly INganhService _nganhService;
        public NganhController(INganhService nganhService)
        {
            _nganhService = nganhService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? khoaId)
        {
            var nganhs = khoaId.HasValue ? 
                await _nganhService.GetNganhsByKhoaIdAsync(khoaId.Value) :
                await _nganhService.GetAllNganhsAsync();
            return Ok(nganhs.Select(n => n.ToNganhDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var nganh = await _nganhService.GetNganhByIdAsync(id);
            if (nganh == null)
                return NotFound();
            return Ok(nganh.ToNganhDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNganhDTO createNganhDTO)
        {
            try
            {
                var nganh = await _nganhService.CreateNganhAsync(createNganhDTO.ToNganhFromCreateDTO());
                return CreatedAtAction(nameof(GetById), new { id = nganh.Id }, nganh.ToNganhDTO());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNganhDTO updateNganhDTO)
        {
            try
            {
                var nganh = await _nganhService.UpdateNganhAsync(id, updateNganhDTO);
                return Ok(nganh?.ToNganhDTO());
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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _nganhService.DeleteNganhAsync(id);
                return NoContent();
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

    }
}
