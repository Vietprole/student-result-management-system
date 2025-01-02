using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Khoa;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/khoa")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private readonly IKhoaService _khoaService;
        public KhoaController(IKhoaService khoaService)
        {
            _khoaService = khoaService;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        [Authorize]
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var khoas = await _khoaService.GetAllKhoasAsync();
            var khoaDTOs = khoas.Select(sv => sv.ToKhoaDTO()).ToList();
            return Ok(khoaDTOs);
        }

        [HttpGet("{id}")]
        [Authorize]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var khoa = await _khoaService.GetKhoaByIdAsync(id);
            if (khoa == null)
                return NotFound("Không tìm thấy khoa");
            var khoaDTO = khoa.ToKhoaDTO();
            return Ok(khoaDTO);
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([FromBody] CreateKhoaDTO createKhoaDTO)
        {
            string check=await _khoaService.CheckCreateKhoa(createKhoaDTO);
            if (check!="Khoa hợp lệ")
            {
                return BadRequest(check);
            }
            var khoa = await _khoaService.CreateKhoaAsync(createKhoaDTO.ToKhoaFromCreateDTO());
            if (khoa == null)
            {
                return BadRequest("Không thể tạo khoa mới.");
            }

            return CreatedAtAction(
                nameof(GetById), // Phương thức sẽ trả về thông tin chi tiết về Khoa
                new { id = khoa.Id }, // Truyền id của khoa vừa tạo
                khoa.ToKhoaDTO() // Trả về DTO của khoa vừa tạo
            );
        }

        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateKhoaDTO updateKhoaDTO)
        {
            try {
                var khoaToUpdate = await _khoaService.UpdateKhoaAsync(id, updateKhoaDTO);
                return Ok(khoaToUpdate?.ToKhoaDTO());
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException e){
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try {
                await _khoaService.DeleteKhoaAsync(id);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

    }
}
