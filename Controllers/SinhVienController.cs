using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/sinhvien")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SinhVienController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var sinhViens = await _context.SinhViens.ToListAsync();
            var sinhVienDTOs = sinhViens.Select(sv => sv.ToSinhVienDTO()).ToList();
            return Ok(sinhVienDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.SinhViens.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<SinhVien>> Create([FromBody] CreateSinhVienDTO createSinhVienDTO)
        {
            var sinhVien = createSinhVienDTO.ToSinhVienFromCreateDTO();
            await _context.SinhViens.AddAsync(sinhVien);
            await _context.SaveChangesAsync();
            var sinhVienDTO = sinhVien.ToSinhVienDTO();
            return CreatedAtAction(nameof(GetById), new { id = sinhVien.Id }, sinhVienDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSinhVienDTO updateSinhVienDTO)
        {
            var studentToUpdate = await _context.SinhViens.FindAsync(id);
            if (studentToUpdate == null)
                return NotFound();
            studentToUpdate.Ten = updateSinhVienDTO.Ten;
            await _context.SaveChangesAsync();
            var studentDTO = studentToUpdate.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SinhVien>>> DeleteStudent([FromRoute] int id)
        {
            var studentToDelete = await _context.SinhViens.FindAsync(id);
            if (studentToDelete == null)
                return NotFound();
            _context.SinhViens.Remove(studentToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
