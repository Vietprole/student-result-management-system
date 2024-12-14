using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/taikhoan")]
    [ApiController]
    public class TaiKhoanController:ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;
        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }
        [HttpPost("createTaiKhoan")]
        public async Task<IActionResult> CreateTaiKhoan([FromBody] CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            string checkUsername = await _taiKhoanService.CheckUsername(createTaiKhoanDTO.Username);
            if (checkUsername != "Username hợp lệ")
            {
                return BadRequest(checkUsername);
            }
            string checkPassword = _taiKhoanService.CheckPassword(createTaiKhoanDTO.Password);
            if (checkPassword != "Password hợp lệ")
            {
                return BadRequest(checkPassword);
            }
            var result = await _taiKhoanService.CreateTaiKhoan(createTaiKhoanDTO);
            if (result == null)
            {
                return BadRequest("Username already exists");
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TaiKhoanLoginDTO taiKhoanLoginDTO)
        {
            string checkPassword = _taiKhoanService.CheckPassword(taiKhoanLoginDTO.MatKhau);
            if (checkPassword != "Password hợp lệ")
            {
                return BadRequest(checkPassword);
            }
            var result = await _taiKhoanService.Login(taiKhoanLoginDTO);
            if (result == null)
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(result);
        }
        [HttpDelete("deleteTaiKhoan/{id}")]
        public async Task<IActionResult> DeleteTaiKhoan([FromRoute]int id)
        {
            var result = await _taiKhoanService.DeleteTaiKhoan(id);
            if (!result)
            {
                return BadRequest("Delete failed");
            }
            return Ok("Delete successfully");
        }
    }
}