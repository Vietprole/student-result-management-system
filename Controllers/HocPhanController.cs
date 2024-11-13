using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/hocphan")]
    [ApiController]
    public class HocPhanController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public HocPhanController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var hocPhans = await _context.HocPhans.ToListAsync();
            var hocPhanDTOs = hocPhans.Select(sv => sv.ToHocPhanDTO()).ToList();
            return Ok(hocPhanDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.HocPhans.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHocPhanDTO createHocPhanDTO)
        {
            var hocPhan = createHocPhanDTO.ToHocPhanFromCreateDTO();
            await _context.HocPhans.AddAsync(hocPhan);
            await _context.SaveChangesAsync();
            var hocPhanDTO = hocPhan.ToHocPhanDTO();
            return CreatedAtAction(nameof(GetById), new { id = hocPhan.Id }, hocPhanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateHocPhanDTO updateHocPhanDTO)
        {
            var hocPhanToUpdate = await _context.HocPhans.FindAsync(id);
            if (hocPhanToUpdate == null)
                return NotFound();

            hocPhanToUpdate.Ten = updateHocPhanDTO.Ten;
            hocPhanToUpdate.SoTinChi = updateHocPhanDTO.SoTinChi;
            hocPhanToUpdate.LaCotLoi = updateHocPhanDTO.LaCotLoi;
            hocPhanToUpdate.NganhId = updateHocPhanDTO.NganhId;
            
            await _context.SaveChangesAsync();
            var studentDTO = hocPhanToUpdate.ToHocPhanDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var hocPhanToDelete = await _context.HocPhans.FindAsync(id);
            if (hocPhanToDelete == null)
                return NotFound();
            _context.HocPhans.Remove(hocPhanToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
