using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/baikiemtra")]
    [ApiController]
    public class BaiKiemTraController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public BaiKiemTraController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var baiKiemTras = await _context.BaiKiemTras.ToListAsync();
            var baiKiemTraDTOs = baiKiemTras.Select(sv => sv.ToBaiKiemTraDTO()).ToList();
            return Ok(baiKiemTraDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.BaiKiemTras.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToBaiKiemTraDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBaiKiemTraDTO createBaiKiemTraDTO)
        {
            var baiKiemTra = createBaiKiemTraDTO.ToBaiKiemTraFromCreateDTO();
            await _context.BaiKiemTras.AddAsync(baiKiemTra);
            await _context.SaveChangesAsync();
            var baiKiemTraDTO = baiKiemTra.ToBaiKiemTraDTO();
            return CreatedAtAction(nameof(GetById), new { id = baiKiemTra.Id }, baiKiemTraDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBaiKiemTraDTO updateBaiKiemTraDTO)
        {
            var baiKiemTraToUpdate = await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTraToUpdate == null)
                return NotFound();

            baiKiemTraToUpdate.Loai = updateBaiKiemTraDTO.Loai;
            baiKiemTraToUpdate.TrongSo = updateBaiKiemTraDTO.TrongSo;
            baiKiemTraToUpdate.LopHocPhanId = updateBaiKiemTraDTO.LopHocPhanId;
            
            await _context.SaveChangesAsync();
            var studentDTO = baiKiemTraToUpdate.ToBaiKiemTraDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var baiKiemTraToDelete = await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTraToDelete == null)
                return NotFound();
            _context.BaiKiemTras.Remove(baiKiemTraToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
