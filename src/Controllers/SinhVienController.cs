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
        private readonly ISinhVienService    _sinhVienRepository;
        private readonly ITaiKhoanService _taiKhoanRepository;
        public SinhVienController(ApplicationDBContext context, ISinhVienService sinhVienRepository, ITaiKhoanService taiKhoanRepository)
        {
            _context = context;
            _sinhVienRepository = sinhVienRepository;
            _taiKhoanRepository = taiKhoanRepository;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            List<SinhVien> sinhViens = await _sinhVienRepository.GetAllSinhVien();
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
            var student = await _sinhVienRepository.GetById(id);
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
                return StatusCode(500, "Create sinh vien failed");
            }
            TaiKhoan? taiKhoan = await _sinhVienRepository.CreateTaiKhoanSinhVien(createSinhVienDTO);
            if(taiKhoan==null)
            {
                return StatusCode(500, "Create sinh vien failed");
            }

            SinhVien? newSinhVien = await _sinhVienRepository.CreateSinhVien(newSV,taiKhoan);
            if (newSinhVien == null)
            {
                return StatusCode(500, "Create sinh vien failed");
            }
            return CreatedAtAction(
                nameof(GetById), // Phương thức sẽ trả về thông tin chi tiết về SinhVien
                new { id = newSinhVien.Id}, // Truyền id của SinhVien vừa tạo
                newSinhVien.ToSinhVienDTO() // Trả về DTO của SinhVien vừa tạo
            );
        }
        [HttpPut("{id}")] //sua
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSinhVienDTO updateSinhVienDTO)
        {
            var studentToUpdate = await _sinhVienRepository.UpdateSV(id,updateSinhVienDTO);
            if(studentToUpdate==null)
            {
                return NotFound();
            }
            var studentDTO = studentToUpdate.ToSinhVienDTO();
            return Ok(studentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var sinhvienDelete= await _sinhVienRepository.DeleteSV(id);
            if(sinhvienDelete==null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
