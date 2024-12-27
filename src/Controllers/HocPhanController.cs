using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/hocphan")]
    [ApiController]
    // [Authorize]
    public class HocPhanController : ControllerBase
    {
        private readonly IHocPhanService _hocPhanService;
        private readonly IPLOService _pLOService;
        public HocPhanController(IHocPhanService hocPhanService, IPLOService pLOService)
        {
            _hocPhanService = hocPhanService;
            _pLOService = pLOService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? khoaId, [FromQuery] int? nganhId, [FromQuery] bool? laCotLoi)
        {
            List<HocPhanDTO> hocPhanDTOs;
            if (khoaId.HasValue || nganhId.HasValue || laCotLoi.HasValue)
            {
                hocPhanDTOs = await _hocPhanService.GetFilteredHocPhansAsync(khoaId, nganhId, laCotLoi);
            }
            else
            {
                hocPhanDTOs = await _hocPhanService.GetAllHocPhansAsync();
            }
            return Ok(hocPhanDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var hocPhanDTO = await _hocPhanService.GetHocPhanByIdAsync(id);
            if (hocPhanDTO == null)
                return NotFound();
            return Ok(hocPhanDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHocPhanDTO createHocPhanDTO)
        {
            var hocPhanDTO = await _hocPhanService.CreateHocPhanAsync(createHocPhanDTO);
            return CreatedAtAction(nameof(GetById), new { id = hocPhanDTO.Id }, hocPhanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateHocPhanDTO updateHocPhanDTO)
        {
            var hocPhanDTO = await _hocPhanService.UpdateHocPhanAsync(id, updateHocPhanDTO);
            if (hocPhanDTO == null)
                return NotFound();
            return Ok(hocPhanDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _hocPhanService.DeleteHocPhanAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/plo")]
        public async Task<IActionResult> GetPLOs([FromRoute] int id)
        {
            var hocPhan = await _hocPhanService.GetHocPhanByIdAsync(id);
            if (hocPhan == null)
                return NotFound("HocPhan not found");

            var pLODTOs = await _pLOService.GetPLOsByHocPhanIdAsync(id);
            return Ok(pLODTOs);
        }

        // [HttpPost("{id}/plo")]
        // public async Task<IActionResult> AddPLOs([FromRoute] int id, [FromBody] int[] pLOIds)
        // {
        //     var hocPhan = await _hocPhanService.GetHocPhanByIdAsync(id);
        //     if (hocPhan == null)
        //         return NotFound("HocPhan not found");
            
        //     var result = await _hocPhanService.AddPLOsToHocPhanAsync(id, pLOIds);
            
        //     if (!result.IsSuccess)
        //         return NotFound(result.ErrorMessage);

        //     return CreatedAtAction(nameof(GetPLOs), new { id = hocPhan.Id });
        // }

        [HttpPut("{id}/plo")]
        public async Task<IActionResult> UpdatePLOs([FromRoute] int id, [FromBody] int[] pLOIds)
        {
            var hocPhan = await _hocPhanService.GetHocPhanByIdAsync(id);
            if (hocPhan == null)
                return NotFound("HocPhan not found");

            try
            {
                var updatedPLOs = await _hocPhanService.UpdatePLOsOfHocPhanAsync(id, pLOIds);
                return Ok(updatedPLOs);
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
        [HttpGet("notInNganh/{nganhId}")]
        public async Task<IActionResult> GetAllHocPhanNotInNganhId([FromRoute] int nganhId)
        {
            var hocPhanDTOs = await _hocPhanService.GetAllHocPhanNotInNganhId(nganhId);
            return Ok(hocPhanDTOs);
        }

        // [HttpDelete("{id}/plo/{pLOId}")]
        // public async Task<IActionResult> RemovePLO([FromRoute] int id, [FromRoute] int pLOId)
        // {
        //     var result = await _pLOService.RemovePLOFromHocPhanAsync(id, pLOId);
        //     if (!result)
        //         return NotFound("HocPhan or PLO not found");

        //     return NoContent();
        // }
    }
}
