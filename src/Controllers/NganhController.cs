using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/nganh")]
    [ApiController]
    public class NganhController : ControllerBase
    {
        private readonly INganhService _nganhService;
        public NganhController(INganhService nganhService)
        {
            _nganhService = nganhService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int? khoaId)
        {
            var nganhs = khoaId.HasValue ? 
                await _nganhService.GetNganhsByKhoaIdAsync(khoaId.Value) :
                await _nganhService.GetAllNganhsAsync();
            return Ok(nganhs.Select(n => n.ToNganhDTO()));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var nganh = await _nganhService.GetNganhByIdAsync(id);
            if (nganh == null)
                return NotFound("Không tìm thấy ngành");
            return Ok(nganh.ToNganhDTO());
        }

        [HttpPost]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> Create([FromBody] CreateNganhDTO createNganhDTO)
        {
            try
            {
                var nganh = await _nganhService.CreateNganhAsync(createNganhDTO.ToNganhFromCreateDTO());
                return CreatedAtAction(nameof(GetById), new { id = nganh.Id }, nganh.ToNganhDTO());
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNganhDTO updateNganhDTO)
        {
            try
            {
                var nganh = await _nganhService.UpdateNganhAsync(id, updateNganhDTO);
                return Ok(nganh?.ToNganhDTO());
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

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _nganhService.DeleteNganhAsync(id);
                return NoContent();
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

        [HttpGet("{id}/hocphan")]
        [Authorize]
        public async Task<IActionResult> GetHocPhans([FromRoute] int id)
        {
            try {
                var hocPhans = await _nganhService.GetHocPhansInNganhAsync(id);
                return Ok(hocPhans);
            }
            catch (BusinessLogicException ex){
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex){
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("{id}/hocphan")]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> AddHocPhans([FromRoute] int id, [FromBody] int[] hocPhanIds)
        {
            try
            {
                var hocPhans = await _nganhService.AddHocPhansToNganhAsync(id, hocPhanIds);
                return Ok("Thêm học phần thành công!");
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

        [HttpPut("{id}/hocphan")]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> UpdateHocPhans([FromRoute] int id, [FromBody] int[] hocPhanIds)
        {
            try
            {
                var hocPhans = await _nganhService.UpdateHocPhansOfNganhAsync(id, hocPhanIds);
                return Ok(hocPhans);
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

        [HttpDelete("{id}/hocphan/{hocPhanId}")]
        [Authorize(Roles="Admin,PhongDaoTao")]
        public async Task<IActionResult> RemoveHocPhan([FromRoute] int id, [FromRoute] int hocPhanId)
        {
            try
            {
                var result = await _nganhService.RemoveHocPhanFromNganhAsync(id, hocPhanId);
                if (!result)
                    return NotFound();
                return NoContent();
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

        [HttpPatch("{id}/hocphan/cotloi")]
        public async Task<IActionResult> UpdateHocPhanCotLoi([FromRoute] int id, [FromBody] List<UpdateCotLoiDTO> updateCotLoiDTOs)
        {
            try
            {
                var result = await _nganhService.UpdateHocPhanCotLoi(id, updateCotLoiDTOs);
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
    }
}
