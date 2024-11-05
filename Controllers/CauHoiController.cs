using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/cauhoi")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CauHoiController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var cauHois = await _context.CauHois.ToListAsync();
            var cauHoiDTOs = cauHois.Select(sv => sv.ToCauHoiDTO()).ToList();
            return Ok(cauHoiDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.CauHois.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToCauHoiDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCauHoiDTO createCauHoiDTO)
        {
            var cauHoi = createCauHoiDTO.ToCauHoiFromCreateDTO();
            await _context.CauHois.AddAsync(cauHoi);
            await _context.SaveChangesAsync();
            var cauHoiDTO = cauHoi.ToCauHoiDTO();
            return CreatedAtAction(nameof(GetById), new { id = cauHoi.Id }, cauHoiDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoiToUpdate = await _context.CauHois.FindAsync(id);
            if (cauHoiToUpdate == null)
                return NotFound();

            cauHoiToUpdate.Ten = updateCauHoiDTO.Ten;
            cauHoiToUpdate.TrongSo = updateCauHoiDTO.TrongSo;
            cauHoiToUpdate.BaiKiemTraId = updateCauHoiDTO.BaiKiemTraId;
            
            await _context.SaveChangesAsync();
            var studentDTO = cauHoiToUpdate.ToCauHoiDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var cauHoiToDelete = await _context.CauHois.FindAsync(id);
            if (cauHoiToDelete == null)
                return NotFound();
            _context.CauHois.Remove(cauHoiToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
