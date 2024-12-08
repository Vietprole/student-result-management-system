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
        private readonly ApplicationDBContext _context;
        private readonly IBaiKiemTraRepository _IBaiKiemTraRepository;
        private readonly ITokenSerivce _tokenSerivce;
        public BaiKiemTraController(ApplicationDBContext context, IBaiKiemTraRepository IBaiKiemTraRepository, ITokenSerivce tokenSerivce)
        {
            _context = context;
            _IBaiKiemTraRepository = IBaiKiemTraRepository;
            _tokenSerivce = tokenSerivce;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int lopHocPhanId) // async go with Task<> to make function asynchronous
        {
            if(lopHocPhanId<=0)
            {
                return BadRequest();
            }
            var baiKiemTraDTOs = await _IBaiKiemTraRepository.GetAllBaiKiemTraByLopHocPhanId(lopHocPhanId);
            return Ok(baiKiemTraDTOs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var baiKiemTraDTO = await _IBaiKiemTraRepository.GetBaiKiemTra(id);
            if (baiKiemTraDTO == null)
            {
                return NotFound();
            }
            return Ok(baiKiemTraDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBaiKiemTraDTO createBaiKiemTraDTO)
        {
            var baiKiemTraDTO = await _IBaiKiemTraRepository.CreateBaiKiemTra(createBaiKiemTraDTO);
            return CreatedAtAction(nameof(GetById), new { id = baiKiemTraDTO.Id }, baiKiemTraDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBaiKiemTraDTO updateBaiKiemTraDTO)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var fullName = await _tokenSerivce.GetFullNameAndRole(token);
            var baiKiemTraDTO = await _IBaiKiemTraRepository.UpdateBaiKiemTra(id, updateBaiKiemTraDTO);
            if (baiKiemTraDTO == null)
            {
                return NotFound();
            }
            return Ok(baiKiemTraDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var baiKiemTraDTO = await _IBaiKiemTraRepository.DeleteBaiKiemTra(id);
            if (baiKiemTraDTO == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
