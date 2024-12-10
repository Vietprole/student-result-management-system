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

namespace Student_Result_Management_System.Controllers
{
    [Route("api/cauhoi")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAll([FromQuery] int? baiKiemTraId)
        {
            List<CauHoiDTO> cauHoiDTOs;
            if (baiKiemTraId.HasValue)
            {
                cauHoiDTOs = await _cauHoiService.GetAllCauHoiByBaiKiemTraId(baiKiemTraId.Value);
            }
            else
            {
                cauHoiDTOs = await _cauHoiService.GetAllCauHoi();
            }
            return Ok(cauHoiDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cauHoiDTO = await _cauHoiService.GetCauHoi(id);
            if (cauHoiDTO == null)
                return NotFound();
            return Ok(cauHoiDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCauHoiDTO createCauHoiDTO)
        {
            var cauHoiDTO = await _cauHoiService.CreateCauHoi(createCauHoiDTO);
            return CreatedAtAction(nameof(GetById), new { id = cauHoiDTO.Id }, cauHoiDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoiDTO = await _cauHoiService.UpdateCauHoi(id, updateCauHoiDTO);
            if (cauHoiDTO == null)
                return NotFound();
            return Ok(cauHoiDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _cauHoiService.DeleteCauHoi(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/view-clos")]
        public async Task<IActionResult> GetCLOs([FromRoute] int id)
        {
            var cauHoi = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            var cLODTOs = await _cLOService.GetCLOsByCauHoiIdAsync(id);
            return Ok(cLODTOs);
        }


        [HttpPost("{id}/add-clos")]
        public async Task<IActionResult> AddCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        {
            var cauHoi = await _cauHoiService.GetCauHoiByIdAsync(id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            // AddCLOsToCauHoi() is a method in the CauHoiService that adds CLOs to a CauHoi

            foreach (var cLOId in cLOIds)
            {
                var cLO = await _cLOService.GetCLOByIdAsync(cLOId);
                if (cLO == null)
                    return NotFound($"CLO with ID {cLOId} not found");

                // Check if the CLO is already in the CauHoi to avoid duplicates
                if (!cauHoi.CLOs.Contains(cLO))
                {
                    cauHoi.CLOs.Add(cLO);
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCLOs), new { id = cauHoi.Id }, cauHoi.CLOs.Select(sv => sv.ToCLODTO()).ToList());
        }

        [HttpPut("{id}/update-clos")]
        public async Task<IActionResult> UpdateCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        {
            var cauHoi = await _context.CauHois
                .Include(p => p.CLOs)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (cauHoi == null)
                return NotFound("Cau Hoi not found");

            // Get existing CLO IDs
            var existingCLOIds = cauHoi.CLOs.Select(c => c.Id).ToList();
            
            // Find IDs to add and remove
            var idsToAdd = cLOIds.Except(existingCLOIds);
            var idsToRemove = existingCLOIds.Except(cLOIds);

            // Remove CLOs
            foreach (var removeId in idsToRemove)
            {
                var cLOToRemove = cauHoi.CLOs.First(c => c.Id == removeId);
                cauHoi.CLOs.Remove(cLOToRemove);
            }

            // Add new CLOs
            foreach (var addId in idsToAdd)
            {
                var cLO = await _context.CLOs.FindAsync(addId);
                if (cLO == null)
                    return NotFound($"CLO with ID {addId} not found");
                    
                cauHoi.CLOs.Add(cLO);
            }

            await _context.SaveChangesAsync();
            return Ok(cauHoi.CLOs.Select(c => c.ToCLODTO()).ToList());
        }

        [HttpDelete("{id}/remove-clo/{cLOId}")]
        public async Task<IActionResult> RemoveCLO([FromRoute] int id, [FromRoute] int cLOId)
        {
            var cauHoi = await _context.CauHois
                .Include(lhp => lhp.CLOs) // Include CLOs to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            var cLO = await _context.CLOs.FindAsync(cLOId);
            if (cLO == null)
                return NotFound($"CLO with ID {cLOId} not found");

            // Check if the HocPhan is in the CTDT to avoid removing non-existing HocPhan
            if (!cauHoi.CLOs.Contains(cLO))
            {
                return NotFound($"CLO with ID {cLOId} is not found in CauHoi with ID {id}");
            }
            cauHoi.CLOs.Remove(cLO);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCLOs), new { id = cauHoi.Id }, cauHoi.CLOs.Select(sv => sv.ToCLODTO()).ToList());
        }
    }
}
