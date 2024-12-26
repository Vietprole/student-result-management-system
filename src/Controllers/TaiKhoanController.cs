using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Utils;

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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? chucVuId)
        {
            var result = await _taiKhoanService.GetFilteredTaiKhoans(chucVuId);
            return Ok(result);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaiKhoanDTO updateTaiKhoanDTO)
        {
            try {
                var result = await _taiKhoanService.UpdateTaiKhoan(id, updateTaiKhoanDTO);
                return Ok(result);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoan([FromRoute]int id)
        {
            var result = await _taiKhoanService.DeleteTaiKhoan(id);
            if (!result)
            {
                return BadRequest("Delete failed");
            }
            return Ok("Delete successfully");
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                var userIdInt = int.Parse(userId?? throw new NotFoundException("Không tìm thấy tài khoản này"));
                var result = await _taiKhoanService.ChangePassword(userIdInt, changePasswordDTO);
                return Ok("Đổi mật khẩu thành công");
            } catch(BusinessLogicException ex){
                return BadRequest(ex.Message);
            } catch(NotFoundException ex){
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles="Admin, PhongDaoTao")]
        [HttpPatch("{id}/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromRoute] int id)
        {
            try {
                if (User.IsInRole("PhongDaoTao")){
                    await _taiKhoanService.ResetPasswordForSinhVienGiangVien(id);
                    return Ok("Đặt lại mật khẩu thành công");
                }
                await _taiKhoanService.ResetPassword(id);
                return Ok("Đặt lại mật khẩu thành công");
            } catch(BusinessLogicException ex){
                return BadRequest(ex.Message);
            } catch(NotFoundException ex){
                return NotFound(ex.Message);
            }
        }
    }
}