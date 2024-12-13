using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/sinhvien")]
    [ApiController]
    [Authorize]
    public class SinhVienController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ISinhVienService    _sinhVienService;
        public SinhVienController(ApplicationDBContext context, ISinhVienService sinhVienService)
        {
            _context = context;
            _sinhVienService = sinhVienService;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            List<SinhVien> sinhViens = await _sinhVienService.GetAllSinhVien();
            List<SinhVienDTO> result = new List<SinhVienDTO>();
            foreach(SinhVien sv in sinhViens)
            {
                result.Add(sv.ToSinhVienDTO());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _sinhVienService.GetById(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSinhVienDTO createSinhVienDTO)
        {
           SinhVien? newSV = await _sinhVienRepository.CheckSinhVien(createSinhVienDTO);
           if (newSV == null)
           {
               return StatusCode(500, "Không thể tạo sinh viên mới.");
           }
           var newSinhVien = await _sinhVienRepository.CreateSinhVien(createSinhVienDTO);
            if (newSinhVien == null)
            {
            return StatusCode(500, "Không thể tạo sinh viên mới.");
            }
           return CreatedAtAction(
               nameof(GetById), // Phương thức sẽ trả về thông tin chi tiết về SinhVien
               new { id = newSinhVien.Id}, // Truyền id của SinhVien vừa tạo
               newSinhVien // Trả về DTO của SinhVien vừa tạo
           );
        }
        [HttpPut("{id}")] //sua
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSinhVienDTO updateSinhVienDTO)
        {
            var studentToUpdate = await _sinhVienService.UpdateSV(id,updateSinhVienDTO);
            if(studentToUpdate==null)
            {
                return NotFound();
            }
            var studentDTO = studentToUpdate.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    var sinhvienDelete= await _sinhVienService.DeleteSV(id);
        //    if(sinhvienDelete==null)
        //    {
        //        return NotFound();
        //    }
        //    return NoContent();
        //}
    }
}
