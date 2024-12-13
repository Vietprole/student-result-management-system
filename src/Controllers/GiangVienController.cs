using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/giangvien")]
    [ApiController]
    [Authorize]
    public class GiangVienController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IGiangVienService _giangVienService;
        private readonly IKhoaService _khoaService;
        public GiangVienController(ApplicationDBContext context, IGiangVienService giangVienService,IKhoaService khoaService)
        {
            _context = context;
            _giangVienService = giangVienService;
            _khoaService = khoaService;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAllByRole() // async go with Task<> to make function asynchronous
        {
           var giangViens = await _giangVienRepository.GetAllGiangVien();
           var giangVienDTOs = giangViens.Select(sv => sv.ToGiangVienDTO()).ToList();
           return Ok();
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _giangVienService.GetById(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToGiangVienDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGiangVienDTO createGiangVienDTO)
        {
           GiangVien? gv = await _giangVienRepository.CheckGiangVien(createGiangVienDTO);
           if (gv == null)
           {
               return NotFound("Khoa không tồn tại");
           }
           var newGiangVien = await _giangVienRepository.CreateGiangVien(createGiangVienDTO);
            if (newGiangVien == null)
            {
            return StatusCode(500, "Create giang vien failed");
            }
           return CreatedAtAction(nameof(GetById), new { id = newGiangVien.Id }, newGiangVien);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGiangVienDTO updateGiangVienDTO)
        {
            var giangVienToUpdate = await _giangVienService.UpdateGV(id,updateGiangVienDTO);
            if(giangVienToUpdate==null)
            {
                return NotFound();
            }
            return Ok(giangVienToUpdate.ToGiangVienDTO());
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    var giangVienToDelete = await _giangVienService.DeleteGV(id);
        //    if (giangVienToDelete == null)
        //        return NotFound();
        //    return NoContent();
        //}

    }
}
