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
        private readonly IGiangVienRepository _giangVienRepository;
        private readonly IKhoaRepository _khoaRepository;
        public GiangVienController(ApplicationDBContext context, IGiangVienRepository giangVienRepository,IKhoaRepository khoaRepository)
        {
            _context = context;
            _giangVienRepository = giangVienRepository;
            _khoaRepository = khoaRepository;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAllByRole() // async go with Task<> to make function asynchronous
        {
            var role = User.Claims.Where(c=>c.Type==ClaimTypes.Role).Select(c=>c.Value).FirstOrDefault();
            var nameid = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.NameId).Select(c => c.Value).FirstOrDefault();
            if (role != null && nameid!=null)
            {
               if(role == "TruongKhoa")
                {
                    var khoa= await _khoaRepository.GetKhoaByTruongKhoaId(nameid);
                    if (khoa != null)
                    {
                        var khoaGiangViens = await _giangVienRepository.GetAllByKhoaId(khoa.Id);
                        var gvDTOs = khoaGiangViens.Select(sv => sv.ToGiangVienDTO()).ToList();
                        return Ok(gvDTOs);
                    }
                }else if(role == "GiangVien")
                {
                    
                    
                }

            }
            var giangViens = await _giangVienRepository.GetAllGiangVien();
            var giangVienDTOs = giangViens.Select(sv => sv.ToGiangVienDTO()).ToList();
            return Ok();

        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _giangVienRepository.GetById(id);
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
                return StatusCode(500, "Create giang vien failed");
            }
            TaiKhoan? taiKhoan = await _giangVienRepository.CreateTaiKhoanGiangVien(createGiangVienDTO);
            if(taiKhoan==null)
            {
                return StatusCode(500, "Create giang vien failed");
            }
            GiangVien? newGiangVien = await _giangVienRepository.CreateGiangVien(gv,taiKhoan);
            if (newGiangVien == null)
            {
                return StatusCode(500, "Create giang vien failed");
            }
            var giangVienDTO = newGiangVien.ToGiangVienDTO();
            return CreatedAtAction(nameof(GetById), new { id = newGiangVien.Id }, giangVienDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGiangVienDTO updateGiangVienDTO)
        {
            var giangVienToUpdate = await _giangVienRepository.UpdateGV(id,updateGiangVienDTO);
            if(giangVienToUpdate==null)
            {
                return NotFound();
            }
            return Ok(giangVienToUpdate.ToGiangVienDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var giangVienToDelete = await _giangVienRepository.DeleteGV(id);
            if (giangVienToDelete == null)
                return NotFound();
            return NoContent();
        }

    }
}
