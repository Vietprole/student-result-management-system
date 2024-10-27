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
        public async Task<ActionResult<List<SinhVienDTO>>> GetAll() // async go with Task<> to make function asynchronous
        {
            var sinhViens = await _context.SinhViens.ToListAsync();
            var sinhVienDTOs = sinhViens.Select(sv => sv.ToSinhVienDTO()).ToList();
            return Ok(sinhVienDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<ActionResult<SinhVienDTO>> GetStudent(int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.SinhViens.FindAsync(id);
            if (student == null)
                return NotFound("Student not found");
            var studentDTO = student.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<SinhVien>> AddStudent(SinhVien student)
        {
            await _context.SinhViens.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(await _context.SinhViens.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SinhVien>> UpdateStudent(int id, SinhVien student)
        {
            var studentToUpdate = await _context.SinhViens.FindAsync(id);
            if (studentToUpdate == null)
                return NotFound("Student not found");
            studentToUpdate.Ten = student.Ten;
            await _context.SaveChangesAsync();
            return Ok(studentToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SinhVien>>> DeleteStudent(int id)
        {
            var studentToDelete = await _context.SinhViens.FindAsync(id);
            if (studentToDelete == null)
                return NotFound("Student not found");
            _context.SinhViens.Remove(studentToDelete);
            await _context.SaveChangesAsync();
            return Ok(await _context.SinhViens.ToListAsync());
        }
    }
}
