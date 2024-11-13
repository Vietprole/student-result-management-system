using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/plo")]
    [ApiController]
    public class PLOController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public PLOController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var pLOs = await _context.PLOs.ToListAsync();
            var pLODTOs = pLOs.Select(sv => sv.ToPLODTO()).ToList();
            return Ok(pLODTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.PLOs.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToPLODTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePLODTO createPLODTO)
        {
            var pLO = createPLODTO.ToPLOFromCreateDTO();
            await _context.PLOs.AddAsync(pLO);
            await _context.SaveChangesAsync();
            var pLODTO = pLO.ToPLODTO();
            return CreatedAtAction(nameof(GetById), new { id = pLO.Id }, pLODTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePLODTO updatePLODTO)
        {
            var pLOToUpdate = await _context.PLOs.FindAsync(id);
            if (pLOToUpdate == null)
                return NotFound();

            pLOToUpdate.Ten = updatePLODTO.Ten;
            pLOToUpdate.MoTa = updatePLODTO.MoTa;
            pLOToUpdate.CTDTId = updatePLODTO.CTDTId;
            
            await _context.SaveChangesAsync();
            var studentDTO = pLOToUpdate.ToPLODTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var pLOToDelete = await _context.PLOs.FindAsync(id);
            if (pLOToDelete == null)
                return NotFound();
            _context.PLOs.Remove(pLOToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
