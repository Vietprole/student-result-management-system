using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CTDT;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/ctdt")]
    [ApiController]
    [Authorize]
    public class CTDTController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CTDTController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll([FromQuery] int? nganhId) // async go with Task<> to make function asynchronous
        {

            IQueryable<CTDT> query = _context.CTDTs;
            if (nganhId.HasValue)
            {
                query = query.Where(n => n.NganhId == nganhId.Value);
            }

            var cTDTs = await query.ToListAsync();
            var cTDTDTOs = cTDTs.Select(sv => sv.ToCTDTDTO()).ToList();
            return Ok(cTDTDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.CTDTs.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToCTDTDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCTDTDTO createCTDTDTO)
        {
            var cTDT = createCTDTDTO.ToCTDTFromCreateDTO();
            await _context.CTDTs.AddAsync(cTDT);
            await _context.SaveChangesAsync();
            var cTDTDTO = cTDT.ToCTDTDTO();
            return CreatedAtAction(nameof(GetById), new { id = cTDT.Id }, cTDTDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCTDTDTO updateCTDTDTO)
        {
            var cTDTToUpdate = await _context.CTDTs.FindAsync(id);
            if (cTDTToUpdate == null)
                return NotFound();

            cTDTToUpdate.Ten = updateCTDTDTO.Ten;
            cTDTToUpdate.NganhId = updateCTDTDTO.NganhId;
            
            await _context.SaveChangesAsync();
            var studentDTO = cTDTToUpdate.ToCTDTDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cTDTToDelete = await _context.CTDTs.FindAsync(id);
            if (cTDTToDelete == null)
                return NotFound();
            _context.CTDTs.Remove(cTDTToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/view-hocphans")]
        public async Task<IActionResult> GetHocPhans([FromRoute] int id)
        {
            var cTDT = await _context.CTDTs
                .Include(lhp => lhp.HocPhans)
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cTDT == null)
                return NotFound();

            var hocPhanDTOs = cTDT.HocPhans.Select(sv => sv.ToHocPhanDTO()).ToList();
            return Ok(hocPhanDTOs);
        }


        [HttpPost("{id}/add-hocphans")]
        public async Task<IActionResult> AddHocPhans([FromRoute] int id, [FromBody] int[] hocPhanIds)
        {
            var cTDT = await _context.CTDTs
                .Include(lhp => lhp.HocPhans) // Include HocPhans to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cTDT == null)
                return NotFound("CTDT not found");

            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId);
                if (hocPhan == null)
                    return NotFound($"HocPhan with ID {hocPhanId} not found");

                // Check if the HocPhan is already in the CTDT to avoid duplicates
                if (!cTDT.HocPhans.Contains(hocPhan))
                {
                    cTDT.HocPhans.Add(hocPhan);
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHocPhans), new { id = cTDT.Id }, cTDT.HocPhans.Select(sv => sv.ToHocPhanDTO()).ToList());
        }

        [HttpPut("{id}/update-hocphans")]
        public async Task<IActionResult> UpdateHocPhans([FromRoute] int id, [FromBody] int[] hocPhanIds)
        {
            var cTDT = await _context.CTDTs
                .Include(p => p.HocPhans)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (cTDT == null)
                return NotFound("Hoc phan not found");

            // Get existing HocPhan IDs
            var existingHocPhanIds = cTDT.HocPhans.Select(c => c.Id).ToList();
            
            // Find IDs to add and remove
            var idsToAdd = hocPhanIds.Except(existingHocPhanIds);
            var idsToRemove = existingHocPhanIds.Except(hocPhanIds);

            // Remove HocPhans
            foreach (var removeId in idsToRemove)
            {
                var hocPhanToRemove = cTDT.HocPhans.First(c => c.Id == removeId);
                cTDT.HocPhans.Remove(hocPhanToRemove);
            }

            // Add new HocPhans
            foreach (var addId in idsToAdd)
            {
                var hocPhan = await _context.HocPhans.FindAsync(addId);
                if (hocPhan == null)
                    return NotFound($"HocPhan with ID {addId} not found");
                    
                cTDT.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            return Ok(cTDT.HocPhans.Select(c => c.ToHocPhanDTO()).ToList());
        }

        [HttpDelete("{id}/remove-hocphan/{hocPhanId}")]
        public async Task<IActionResult> RemoveHocPhan([FromRoute] int id, [FromRoute] int hocPhanId)
        {
            var cTDT = await _context.CTDTs
                .Include(lhp => lhp.HocPhans) // Include HocPhans to ensure the collection is loaded
                .FirstOrDefaultAsync(lhp => lhp.Id == id);
            if (cTDT == null)
                return NotFound("CTDT not found");

            var hocPhan = await _context.HocPhans.FindAsync(hocPhanId);
            if (hocPhan == null)
                return NotFound($"HocPhan with ID {hocPhanId} not found");

            // Check if the HocPhan is in the CTDT to avoid removing non-existing HocPhan
            if (!cTDT.HocPhans.Contains(hocPhan))
            {
                return NotFound($"HocPhan with ID {hocPhanId} is not found in CTDT with ID {id}");
            }

            cTDT.HocPhans.Remove(hocPhan);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHocPhans), new { id = cTDT.Id }, cTDT.HocPhans.Select(sv => sv.ToHocPhanDTO()).ToList());
        }
    }
}
