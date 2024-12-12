using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Khoa;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/khoa")]
    [ApiController]
    // [Authorize]
    public class KhoaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IKhoaService _khoaRepository;
        public KhoaController(ApplicationDBContext context,IKhoaService khoaRepository)
        {
            _context = context;
            _khoaRepository = khoaRepository;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var khoas = await _khoaRepository.GetListKhoa();
            var khoaDTOs = khoas.Select(sv => sv.ToKhoaDTO()).ToList();
            return Ok(khoaDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.Khoas.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToKhoaDTO();
            return Ok(studentDTO);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateKhoaDTO createKhoaDTO)
        //{
        //    var khoa=await _khoaRepository.CreateKhoa(createKhoaDTO.ToKhoaFromCreateDTO());
        //    if (khoa == null)
        //    {
        //        return BadRequest("Không thể tạo khoa mới.");
        //    }

        //    return CreatedAtAction(
        //        nameof(GetById), // Phương thức sẽ trả về thông tin chi tiết về Khoa
        //        new { id = khoa.Id }, // Truyền id của khoa vừa tạo
        //        khoa.ToKhoaDTO() // Trả về DTO của khoa vừa tạo
        //    );
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateKhoaDTO updateKhoaDTO)
        //{
        //    var khoaToUpdate = await _context.Khoas.FindAsync(id);
        //    if (khoaToUpdate == null)
        //        return NotFound();

        //    khoaToUpdate.Ten = updateKhoaDTO.Ten;
        //    khoaToUpdate.MaKhoa = updateKhoaDTO.MaKhoa;
        //    khoaToUpdate.VietTat = updateKhoaDTO.VietTat;
            
        //    await _context.SaveChangesAsync();
        //    var studentDTO = khoaToUpdate.ToKhoaDTO();
        //    return Ok(studentDTO);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var khoaToDelete = await _context.Khoas.FindAsync(id);
            if (khoaToDelete == null)
                return NotFound();
            _context.Khoas.Remove(khoaToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
