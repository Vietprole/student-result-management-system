using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Mappers;

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
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var nganhs = await _context.Nganhs.ToListAsync();
            var nganhDTOs = nganhs.Select(sv => sv.ToNganhDTO()).ToList();
            return Ok(nganhDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.Nganhs.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToNganhDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNganhDTO createNganhDTO)
        {
            var nganh = createNganhDTO.ToNganhFromCreateDTO();
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();
            var nganhDTO = nganh.ToNganhDTO();
            return CreatedAtAction(nameof(GetById), new { id = nganh.Id }, nganhDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNganhDTO updateNganhDTO)
        {
            var nganhToUpdate = await _context.Nganhs.FindAsync(id);
            if (nganhToUpdate == null)
                return NotFound();

            nganhToUpdate.Ten = updateNganhDTO.Ten;
            nganhToUpdate.KhoaId = updateNganhDTO.KhoaId;
            
            await _context.SaveChangesAsync();
            var studentDTO = nganhToUpdate.ToNganhDTO();
            return Ok(studentDTO);
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
