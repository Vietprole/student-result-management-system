using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/hocphan")]
    [ApiController]
    public class HocPhanController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public HocPhanController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll([FromQuery] int? khoaId) // async go with Task<> to make function asynchronous
        {
            IQueryable<HocPhan> query = _context.HocPhans;
            
            if (khoaId.HasValue)
            {
                query = query.Where(n => n.KhoaId == khoaId.Value);
            }

            var hocPhans = await query.ToListAsync();
            var hocPhanDTOs = hocPhans.Select(sv => sv.ToHocPhanDTO()).ToList();
            return Ok(hocPhanDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.HocPhans.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHocPhanDTO createHocPhanDTO)
        {
            var hocPhan = createHocPhanDTO.ToHocPhanFromCreateDTO();
            await _context.HocPhans.AddAsync(hocPhan);
            await _context.SaveChangesAsync();
            var hocPhanDTO = hocPhan.ToHocPhanDTO();
            return CreatedAtAction(nameof(GetById), new { id = hocPhan.Id }, hocPhanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateHocPhanDTO updateHocPhanDTO)
        {
            var hocPhanToUpdate = await _context.HocPhans.FindAsync(id);
            if (hocPhanToUpdate == null)
                return NotFound();

            hocPhanToUpdate.Ten = updateHocPhanDTO.Ten;
            hocPhanToUpdate.SoTinChi = updateHocPhanDTO.SoTinChi;
            hocPhanToUpdate.LaCotLoi = updateHocPhanDTO.LaCotLoi;
            hocPhanToUpdate.KhoaId = updateHocPhanDTO.KhoaId;
            
            await _context.SaveChangesAsync();
            var studentDTO = hocPhanToUpdate.ToHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var hocPhanToDelete = await _context.HocPhans.FindAsync(id);
            if (hocPhanToDelete == null)
                return NotFound();
            _context.HocPhans.Remove(hocPhanToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/view-plos")]
        public async Task<IActionResult> GetPLOs([FromRoute] int id)
        {
            var hocPhan = await _context.HocPhans
                .Include(lhp => lhp.PLOs)
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (hocPhan == null)
                return NotFound();

            var pLODTOs = hocPhan.PLOs.Select(sv => sv.ToPLODTO()).ToList();
            return Ok(pLODTOs);
        }


        [HttpPost("{id}/add-plos")]
        public async Task<IActionResult> AddPLOs([FromRoute] int id, [FromBody] int[] pLOIds)
        {
            var hocPhan = await _context.HocPhans
                .Include(lhp => lhp.PLOs) // Include PLOs to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (hocPhan == null)
                return NotFound("HocPhan not found");

            foreach (var pLOId in pLOIds)
            {
                var pLO = await _context.PLOs.FindAsync(pLOId);
                if (pLO == null)
                    return NotFound($"PLO with ID {pLOId} not found");

                // Check if the PLO is already in the HocPhan to avoid duplicates
                if (!hocPhan.PLOs.Contains(pLO))
                {
                    hocPhan.PLOs.Add(pLO);
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPLOs), new { id = hocPhan.Id }, hocPhan.PLOs.Select(sv => sv.ToPLODTO()).ToList());
        }

        [HttpPut("{id}/update-plos")]
        public async Task<IActionResult> UpdatePLOs([FromRoute] int id, [FromBody] int[] pLOIds)
        {
            var hocPhan = await _context.HocPhans
                .Include(p => p.PLOs)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (hocPhan == null)
                return NotFound("Lop hoc phan not found");

            // Get existing PLO IDs
            var existingPLOIds = hocPhan.PLOs.Select(c => c.Id).ToList();
            
            // Find IDs to add and remove
            var idsToAdd = pLOIds.Except(existingPLOIds);
            var idsToRemove = existingPLOIds.Except(pLOIds);

            // Remove PLOs
            foreach (var removeId in idsToRemove)
            {
                var pLOToRemove = hocPhan.PLOs.First(c => c.Id == removeId);
                hocPhan.PLOs.Remove(pLOToRemove);
            }

            // Add new PLOs
            foreach (var addId in idsToAdd)
            {
                var pLO = await _context.PLOs.FindAsync(addId);
                if (pLO == null)
                    return NotFound($"PLO with ID {addId} not found");
                    
                hocPhan.PLOs.Add(pLO);
            }

            await _context.SaveChangesAsync();
            return Ok(hocPhan.PLOs.Select(c => c.ToPLODTO()).ToList());
        }

        [HttpDelete("{id}/remove-plo/{pLOId}")]
        public async Task<IActionResult> RemovePLO([FromRoute] int id, [FromRoute] int pLOId)
        {
            var hocPhan = await _context.HocPhans
                .Include(lhp => lhp.PLOs) // Include PLOs to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (hocPhan == null)
                return NotFound("HocPhan not found");

            var pLO = await _context.PLOs.FindAsync(pLOId);
            if (pLO == null)
                return NotFound($"PLO with ID {pLOId} not found");

            // Check if the HocPhan is in the CTDT to avoid removing non-existing HocPhan
            if (!hocPhan.PLOs.Contains(pLO))
            {
                return NotFound($"PLO with ID {pLOId} is not found in HocPhan with ID {id}");
            }
            hocPhan.PLOs.Remove(pLO);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPLOs), new { id = hocPhan.Id }, hocPhan.PLOs.Select(sv => sv.ToPLODTO()).ToList());
        }
    }
}
