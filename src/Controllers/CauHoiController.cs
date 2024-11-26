using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/cauhoi")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CauHoiController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll([FromQuery] int? baiKiemTraId) // async go with Task<> to make function asynchronous
        {
            IQueryable<CauHoi> query = _context.CauHois;
            if (baiKiemTraId.HasValue)
            {
                query = query.Where(n => n.BaiKiemTraId == baiKiemTraId.Value);
            }

            var cauHois = await query.ToListAsync();
            var cauHoiDTOs = cauHois.Select(sv => sv.ToCauHoiDTO()).ToList();
            return Ok(cauHoiDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.CauHois.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToCauHoiDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCauHoiDTO createCauHoiDTO)
        {
            var cauHoi = createCauHoiDTO.ToCauHoiFromCreateDTO();
            await _context.CauHois.AddAsync(cauHoi);
            await _context.SaveChangesAsync();
            var cauHoiDTO = cauHoi.ToCauHoiDTO();
            return CreatedAtAction(nameof(GetById), new { id = cauHoi.Id }, cauHoiDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoiToUpdate = await _context.CauHois.FindAsync(id);
            if (cauHoiToUpdate == null)
                return NotFound();

            cauHoiToUpdate.Ten = updateCauHoiDTO.Ten;
            cauHoiToUpdate.TrongSo = updateCauHoiDTO.TrongSo;
            cauHoiToUpdate.BaiKiemTraId = updateCauHoiDTO.BaiKiemTraId;
            
            await _context.SaveChangesAsync();
            var studentDTO = cauHoiToUpdate.ToCauHoiDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cauHoiToDelete = await _context.CauHois.FindAsync(id);
            if (cauHoiToDelete == null)
                return NotFound();
            _context.CauHois.Remove(cauHoiToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/view-clos")]
        public async Task<IActionResult> GetCLOs([FromRoute] int id)
        {
            var cauHoi = await _context.CauHois
                .Include(lhp => lhp.CLOs)
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cauHoi == null)
                return NotFound();

            var cLODTOs = cauHoi.CLOs.Select(sv => sv.ToCLODTO()).ToList();
            return Ok(cLODTOs);
        }


        [HttpPost("{id}/add-clos")]
        public async Task<IActionResult> AddCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        {
            var cauHoi = await _context.CauHois
                .Include(lhp => lhp.CLOs) // Include CLOs to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cauHoi == null)
                return NotFound("CauHoi not found");

            foreach (var cLOId in cLOIds)
            {
                var cLO = await _context.CLOs.FindAsync(cLOId);
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
