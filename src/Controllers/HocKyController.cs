using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.HocKy;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/hocky")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class HocKyController:ControllerBase
    {
        private readonly IHocKyService _hocKyService;
        public HocKyController(IHocKyService hocKyService)
        {
            _hocKyService = hocKyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hocKyDTOs = await _hocKyService.GetAllHocKyDTO();
            return Ok(hocKyDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var hocKyDTO = await _hocKyService.GetHocKyDTO(id);
            if (hocKyDTO == null)
            {
                return NotFound();
            }
            return Ok(hocKyDTO);
        }
        //[HttpGet("{id}/duocsuadiem")]
        //public async Task<IActionResult> CheckDuocSuaDiem([FromRoute] int id)
        //{
        //    bool duocSuaDiem = await _hocKyService.DuocSuaDiem(id);
        //    if(duocSuaDiem == false)
        //    {
        //        return StatusCode(403, "Không được sửa điểm");
        //    }
        //    return Ok();
        //}
        [HttpPost]
        public async Task<IActionResult> AddHocKy([FromBody] CreateHocKyDTO newHocKyDTO)
        {
            bool CheckTenHocKy = _hocKyService.CheckTenHocKy(newHocKyDTO.Ten);
            if (CheckTenHocKy == false)
            {
                return BadRequest("Tên học kỳ không hợp lệ");
            }

            if (newHocKyDTO.NamHoc<2000 || newHocKyDTO.NamHoc>DateTime.Now.Year)
            {
                return BadRequest("Năm học không hợp lệ");
            }
            var hocKyDTO = await _hocKyService.AddHocKyDTO(newHocKyDTO);
            if (hocKyDTO == null)
            {
                return BadRequest("Không thế tạo học kỳ mới");
            }
            return Ok(hocKyDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHocKy([FromRoute] int id, [FromBody] CreateHocKyDTO newHocKyDTO)
        {
            var hocKyDTO = await _hocKyService.UpdateHocKyDTO(id, newHocKyDTO);
            if (hocKyDTO == null)
            {
                return NotFound();
            }
            return Ok(hocKyDTO);
        }
        [HttpPut("{id}/hansuadiem")]
        public async Task<IActionResult> UpdateHanSuaDiem([FromRoute] int id, [FromBody] DateOnly hanSuaDiem)
        {
            var hanSuaDiemAsDateTime = hanSuaDiem.ToDateTime(TimeOnly.MinValue);

            var hocKyDTO = await _hocKyService.UpdateHanSuaDiem(id, hanSuaDiemAsDateTime);
            if (!hocKyDTO)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpPut("{id}/hansuacongthucdiem")]
        public async Task<IActionResult> UpdateHanSuaCongThucDiem([FromRoute] int id, [FromBody] DateOnly hanSuaCongThucDiem)
        {
            var hanSuaCongThucDiemAsDateTime = hanSuaCongThucDiem.ToDateTime(TimeOnly.MinValue);

            var hocKyDTO = await _hocKyService.UpdateHanSuaCongThucDiem(id, hanSuaCongThucDiemAsDateTime);
            if (!hocKyDTO)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocKy([FromRoute] int id)
        {
            var hocKyDTO = await _hocKyService.DeleteHocKyDTO(id);
            if (hocKyDTO == false)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}