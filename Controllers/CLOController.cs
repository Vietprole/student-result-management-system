using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/clo")]
    [ApiController]
    public class CLOController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CLOController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var cLOs = await _context.CLOs.ToListAsync();
            var cLODTOs = cLOs.Select(sv => sv.ToCLODTO()).ToList();
            return Ok(cLODTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.CLOs.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToCLODTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCLODTO createCLODTO)
        {
            var cLO = createCLODTO.ToCLOFromCreateDTO();
            await _context.CLOs.AddAsync(cLO);
            await _context.SaveChangesAsync();
            var cLODTO = cLO.ToCLODTO();
            return CreatedAtAction(nameof(GetById), new { id = cLO.Id }, cLODTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCLODTO updateCLODTO)
        {
            var cLOToUpdate = await _context.CLOs.FindAsync(id);
            if (cLOToUpdate == null)
                return NotFound();

            cLOToUpdate.Ten = updateCLODTO.Ten;
            cLOToUpdate.MoTa = updateCLODTO.MoTa;
            cLOToUpdate.LopHocPhanId = updateCLODTO.LopHocPhanId;
            
            await _context.SaveChangesAsync();
            var studentDTO = cLOToUpdate.ToCLODTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cLOToDelete = await _context.CLOs.FindAsync(id);
            if (cLOToDelete == null)
                return NotFound();
            _context.CLOs.Remove(cLOToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
