using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.KiHoc;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/kihoc")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class KiHocController:ControllerBase
    {
        private readonly IKiHocRepository _kiHocRepository;
        public KiHocController(IKiHocRepository kiHocRepository)
        {
            _kiHocRepository = kiHocRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kiHocDTOs = await _kiHocRepository.GetAllKiHocDTO();
            return Ok(kiHocDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var kiHocDTO = await _kiHocRepository.GetKiHocDTO(id);
            if (kiHocDTO == null)
            {
                return NotFound();
            }
            return Ok(kiHocDTO);
        }
        [HttpGet("{id}/duocsuadiem")]
        public async Task<IActionResult> CheckDuocSuaDiem([FromRoute] int id)
        {
            bool duocSuaDiem = await _kiHocRepository.DuocSuaDiem(id);
            if(duocSuaDiem == false)
            {
                return StatusCode(403, "Không được sửa điểm");
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddKiHoc([FromBody] NewKiHocDTO newKiHocDTO)
        {
            var kiHocDTO = await _kiHocRepository.AddKiHocDTO(newKiHocDTO);
            return Ok(kiHocDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKiHoc([FromRoute] int id, [FromBody] NewKiHocDTO newKiHocDTO)
        {
            var kiHocDTO = await _kiHocRepository.UpdateKiHocDTO(id, newKiHocDTO);
            if (kiHocDTO == null)
            {
                return NotFound();
            }
            return Ok(kiHocDTO);
        }
        [HttpPut("{id}/hansuadiem")]
        public async Task<IActionResult> UpdateHanSuaDiem([FromRoute] int id, [FromBody] DateOnly hanSuaDiem)
        {
            var hanSuaDiemAsDateTime = hanSuaDiem.ToDateTime(TimeOnly.MinValue);

            var kiHocDTO = await _kiHocRepository.UpdateHanSuaDiem(id, hanSuaDiemAsDateTime);
            if (!kiHocDTO)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpPut("{id}/hansuacongthucdiem")]
        public async Task<IActionResult> UpdateHanSuaCongThucDiem([FromRoute] int id, [FromBody] DateOnly hanSuaCongThucDiem)
        {
            var hanSuaCongThucDiemAsDateTime = hanSuaCongThucDiem.ToDateTime(TimeOnly.MinValue);

            var kiHocDTO = await _kiHocRepository.UpdateHanSuaCongThucDiem(id, hanSuaCongThucDiemAsDateTime);
            if (!kiHocDTO)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKiHoc([FromRoute] int id)
        {
            var kiHocDTO = await _kiHocRepository.DeleteKiHocDTO(id);
            if (kiHocDTO == false)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}