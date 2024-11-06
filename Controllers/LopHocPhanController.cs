using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/lophocphan")]
    [ApiController]
    public class LopHocPhanController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public LopHocPhanController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var lopHocPhans = await _context.LopHocPhans.ToListAsync();
            var lopHocPhanDTOs = lopHocPhans.Select(sv => sv.ToLopHocPhanDTO()).ToList();
            return Ok(lopHocPhanDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.LopHocPhans.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToLopHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLopHocPhanDTO createLopHocPhanDTO)
        {
            var lopHocPhan = createLopHocPhanDTO.ToLopHocPhanFromCreateDTO();
            await _context.LopHocPhans.AddAsync(lopHocPhan);
            await _context.SaveChangesAsync();
            var lopHocPhanDTO = lopHocPhan.ToLopHocPhanDTO();
            return CreatedAtAction(nameof(GetById), new { id = lopHocPhan.Id }, lopHocPhanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLopHocPhanDTO updateLopHocPhanDTO)
        {
            var lopHocPhanToUpdate = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhanToUpdate == null)
                return NotFound();

            lopHocPhanToUpdate.Ten = updateLopHocPhanDTO.Ten;
            lopHocPhanToUpdate.HocPhanId = updateLopHocPhanDTO.HocPhanId;
            
            await _context.SaveChangesAsync();
            var studentDTO = lopHocPhanToUpdate.ToLopHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var lopHocPhanToDelete = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhanToDelete == null)
                return NotFound();
            _context.LopHocPhans.Remove(lopHocPhanToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
