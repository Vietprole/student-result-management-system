using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CTDT;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/ctdt")]
    [ApiController]
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
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var cTDTs = await _context.CTDTs.ToListAsync();
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
            cTDTToUpdate.KhoaId = updateCTDTDTO.KhoaId;
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

    }
}
