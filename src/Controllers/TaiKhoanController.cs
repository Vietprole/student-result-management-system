using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/taikhoan")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        private readonly IChucVuRepository _chucVuRepository;

        public TaiKhoanController(ITaiKhoanRepository taiKhoanRepository, IChucVuRepository chucVuRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _chucVuRepository = chucVuRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TaiKhoanLoginDTO taiKhoanDTO)
        {
            var taiKhoan = await _taiKhoanRepository.CheckLogin(taiKhoanDTO.TenDangNhap, taiKhoanDTO.MatKhau);
            if (taiKhoan == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            return Ok(new { Message = "Login successfully"});
        }
        [HttpPost]
        [Route("{chucvuId}")] //Tao tai khoan
        public async Task<IActionResult> CreateTaiKhoan([FromBody] CreateTaiKhoanDTO taiKhoanDTO,[FromRoute] int chucvuId)
        {
            var chucVu = await _chucVuRepository.ChucVuExists(chucvuId);
            if (chucVu == false)
            {
                return NotFound(new { Message = "Chuc vu khong ton tai" });
            }
            var taiKhoan = taiKhoanDTO.ToTaiKhoanFromCreateDTO(chucvuId);
            await _taiKhoanRepository.CreateTaiKhoan(taiKhoan);
            return Ok(new { Message = "Tao tai khoan thanh cong" });
        }
        [HttpPut]
        [Route("{taiKhoanId}")] //Cap nhat tai khoan
        public async Task<IActionResult> UpdateMatKhau([FromBody] UpdateMatKhauDTO taiKhoanDTO, [FromRoute] int taiKhoanId)
        {
            var taiKhoan = await _taiKhoanRepository.TaiKhoanExists(taiKhoanId);
            if (taiKhoan == false)
            {
                return NotFound(new { Message = "Tai khoan khong ton tai" });
            }
            await _taiKhoanRepository.UpdateMatKhau(taiKhoanId, taiKhoanDTO.MatKhau);
            return Ok(new { Message = "Cap nhat mat khau thanh cong" });
        }
        [HttpDelete]
        [Route("{taiKhoanId}")] //Xoa tai khoan
        public async Task<IActionResult> DeleteTaiKhoan([FromRoute] int taiKhoanId)
        {
            var taiKhoan = await _taiKhoanRepository.TaiKhoanExists(taiKhoanId);
            if (taiKhoan == false)
            {
                return NotFound(new { Message = "Tai khoan khong ton tai" });
            }
            await _taiKhoanRepository.DeleteTaiKhoan(taiKhoanId);
            return Ok(new { Message = "Xoa tai khoan thanh cong" });
        }
    }
}
