using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/baikiemtra")]
    [ApiController]
    [Authorize]
    public class BaiKiemTraController : ControllerBase
    {
        private readonly IBaiKiemTraService _baiKiemTraService;
        private readonly ITokenService _tokenService;
        public BaiKiemTraController(IBaiKiemTraService IBaiKiemTraService, ITokenService tokenSerivce)
        {
            _baiKiemTraService = IBaiKiemTraService;
            _tokenService = tokenSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int lopHocPhanId) // async go with Task<> to make function asynchronous
        {
            if(lopHocPhanId<=0)
            {
                return BadRequest("Lớp Học Phần Id phải lớn hơn 0");
            }
            var baiKiemTraDTOs = await _baiKiemTraService.GetBaiKiemTrasByLopHocPhanIdAsync(lopHocPhanId);
            return Ok(baiKiemTraDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var baiKiemTraDTO = await _baiKiemTraService.GetBaiKiemTraByIdAsync(id);
            if (baiKiemTraDTO == null)
            {
                return NotFound();
            }
            return Ok(baiKiemTraDTO);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateBaiKiemTraDTO createBaiKiemTraDTO)
        //{
        //    var checkDuplicate = await _baiKiemTraService.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(createBaiKiemTraDTO.Loai, createBaiKiemTraDTO.LopHocPhanId);
        //    if (checkDuplicate)
        //    {
        //        return BadRequest($"Bài kiểm tra với loại {createBaiKiemTraDTO.Loai} đã tồn tại trong lớp học phần");
        //    }
        //    var baiKiemTraDTO = await _baiKiemTraService.CreateBaiKiemTraAsync(createBaiKiemTraDTO);
        //    return CreatedAtAction(nameof(GetById), new { id = baiKiemTraDTO.Id }, baiKiemTraDTO);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBaiKiemTraDTO updateBaiKiemTraDTO)
        {
            var baiKiemTra = await _baiKiemTraService.GetBaiKiemTraByIdAsync(id);
            if (baiKiemTra == null)
            {
                return NotFound("Bài kiểm tra không tồn tại");
            }
            var checkDuplicate = await _baiKiemTraService.CheckDuplicateBaiKiemTraLoaiInLopHocPhan(updateBaiKiemTraDTO.Loai, baiKiemTra.LopHocPhanId);
            if (checkDuplicate)
            {
                return BadRequest($"Bài kiểm tra với loại {updateBaiKiemTraDTO.Loai} đã tồn tại trong lớp học phần");
            }
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var fullName = await _tokenService.GetFullNameAndRole(token);
            var baiKiemTraDTO = await _baiKiemTraService.UpdateBaiKiemTraAsync(id, updateBaiKiemTraDTO);
            if (baiKiemTraDTO == null)
            {
                return NotFound();
            }
            return Ok(baiKiemTraDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var baiKiemTraDTO = await _baiKiemTraService.DeleteBaiKiemTraAsync(id);
            if (baiKiemTraDTO == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
