using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Result_Management_System.DTOs.ChucVu;
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
        private readonly ITokenSerivce _tokenService;
        private readonly IChucVuRepository _chucVuRepository;

        public TaiKhoanController(ITaiKhoanRepository taiKhoanRepository, ITokenSerivce tokenService, IChucVuRepository chucVuRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _tokenService = tokenService;
            _chucVuRepository = chucVuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile([FromRoute] string userId)
        {
            var exists = await _taiKhoanRepository.GetById(userId);
            if (exists == null)
            {
                return NotFound("User not found");
            }
            var roles = await _taiKhoanRepository.GetRoles(exists);
            var role = roles.FirstOrDefault();
            if (role == null)
            {
                return NotFound();
            }
            var dto = exists.ToTaiKhoanProfileDTO(role);
            return Ok(dto);
        }
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(createTaiKhoanDTO.TenChucVu))
                {
                    return StatusCode(500,"Role name is required");
                }
                if(string.IsNullOrEmpty(createTaiKhoanDTO.HovaTen))
                {
                    return StatusCode(500,"Full name is required");
                }
                var rs = await _chucVuRepository.GetIdChucVu(createTaiKhoanDTO.TenChucVu);
                if (rs == null)
                {
                    return StatusCode(500,"Role not found");
                }

                if (string.IsNullOrEmpty(createTaiKhoanDTO.Username))
                {
                    return StatusCode(500,"Username name is required");
                }
                var user = await _taiKhoanRepository.CheckUser(createTaiKhoanDTO.Username);
                if (user != null)
                {
                    return StatusCode(500,"Username already exists");
                }

                var result = await _taiKhoanRepository.CreateUser(createTaiKhoanDTO, createTaiKhoanDTO.TenChucVu.ToChucVuDTOFromString());
                if (result == null)
                {
                    return StatusCode(500,"Create user failed");
                }
                return Ok(
                    new NewTaiKhoanDTO
                    {
                        Username = result?.UserName ?? string.Empty,
                        Token = result != null ? await _tokenService.CreateToken(result) : string.Empty
                    }
                );
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TaiKhoanLoginDTO taiKhoanLoginDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _taiKhoanRepository.CheckUser(taiKhoanLoginDTO.TenDangNhap);
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }
            var result = await _taiKhoanRepository.CheckPassword(user,taiKhoanLoginDTO.MatKhau);
            if (!result)
            {
                return Unauthorized("Username not found and/or password is incorrect");
            }
            return Ok(
                new NewTaiKhoanDTO
                {
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                }
            );
        }
        [HttpDelete]
        [Route("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {
            var user = await _taiKhoanRepository.CheckUser(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var result = await _taiKhoanRepository.DeleteUser(user);
            if (result == null)
            {
                return StatusCode(500,"Delete user failed");
            }
            return Ok("Delete user successfully");
        }

    }
}