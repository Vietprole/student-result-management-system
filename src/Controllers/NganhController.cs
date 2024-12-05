using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/nganh")]
    [ApiController]
    [Authorize]
    public class NganhController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public NganhController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll([FromQuery] int? khoaId) // [FromQuery] binds to query parameter
        {
            IQueryable<Nganh> query = _context.Nganhs.Include(n => n.Khoa);

            if (khoaId.HasValue)
            {
                query = query.Where(n => n.KhoaId == khoaId.Value);
            }

            var nganhs = await query.ToListAsync(); // Use query instead of _context.Nganhs
            var nganhDTOs = nganhs.Select(sv => sv.ToNganhDTO()).ToList();
            return Ok(nganhDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var nganh = await _context.Nganhs
                .Include(n => n.Khoa)
                .FirstOrDefaultAsync(n => n.Id == id);
            if (nganh == null)
                return NotFound();
            var nganhDTO = nganh.ToNganhDTO();
            return Ok(nganhDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNganhDTO createNganhDTO)
        {
            var nganh = createNganhDTO.ToNganhFromCreateDTO();
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();

            // Reload entity with Khoa included
            nganh = await _context.Nganhs
                .Include(n => n.Khoa)
                .FirstOrDefaultAsync(n => n.Id == nganh.Id);

            var nganhDTO = nganh?.ToNganhDTO();
            return CreatedAtAction(nameof(GetById), new { id = nganh?.Id }, nganhDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNganhDTO updateNganhDTO)
        {
            var nganhToUpdate = await _context.Nganhs
                .FirstOrDefaultAsync(n => n.Id == id);
            if (nganhToUpdate == null)
                return NotFound();

            nganhToUpdate.Ten = updateNganhDTO.Ten;
            nganhToUpdate.KhoaId = updateNganhDTO.KhoaId;
            
            await _context.SaveChangesAsync();
            // Reload to get updated Khoa information
            nganhToUpdate = await _context.Nganhs
                .Include(n => n.Khoa)
                .FirstOrDefaultAsync(n => n.Id == id);

            var nganhDTO = nganhToUpdate?.ToNganhDTO();
            return Ok(nganhDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var nganhToDelete = await _context.Nganhs.FindAsync(id);
            if (nganhToDelete == null)
                return NotFound();
            _context.Nganhs.Remove(nganhToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
