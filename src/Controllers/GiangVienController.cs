using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/giangvien")]
    [ApiController]
    [Authorize]
    public class GiangVienController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IGiangVienRepository _giangVienRepository;
        public GiangVienController(ApplicationDBContext context, IGiangVienRepository giangVienRepository)
        {
            _context = context;
            _giangVienRepository = giangVienRepository;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var giangViens = await _context.GiangViens.ToListAsync();
            var giangVienDTOs = giangViens.Select(sv => sv.ToGiangVienDTO()).ToList();
            return Ok(giangVienDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.GiangViens.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToGiangVienDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGiangVienDTO createGiangVienDTO)
        {
            GiangVien? gv = await _giangVienRepository.CheckGiangVien(createGiangVienDTO);
            if (gv == null)
            {
                return StatusCode(500, "Create giang vien failed1");
            }
            GiangVien? newGiangVien = await _giangVienRepository.CreateGiangVien(gv);
            if (newGiangVien == null)
            {
                return StatusCode(500, "Create giang vien failed2");
            }
            var giangVienDTO = newGiangVien.ToGiangVienDTO();
            return CreatedAtAction(nameof(GetById), new { id = newGiangVien.Id }, giangVienDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGiangVienDTO updateGiangVienDTO)
        {
            var giangVienToUpdate = await _context.GiangViens.FindAsync(id);
            if (giangVienToUpdate == null)
                return NotFound();

            giangVienToUpdate.Ten = updateGiangVienDTO.Ten;
            giangVienToUpdate.KhoaId = updateGiangVienDTO.KhoaId;
            
            await _context.SaveChangesAsync();
            var studentDTO = giangVienToUpdate.ToGiangVienDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var giangVienToDelete = await _context.GiangViens.FindAsync(id);
            if (giangVienToDelete == null)
                return NotFound();
            _context.GiangViens.Remove(giangVienToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
