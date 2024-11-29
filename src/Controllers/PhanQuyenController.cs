using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.PhanQuyen;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/phanquyen")]
    [ApiController]
    public class PhanQuyenController:ControllerBase
    {
        private readonly IPhanQuyenRepository _phanQuyenRepository;
        private readonly IChucVuRepository _chucVuRepository;
        public PhanQuyenController(IPhanQuyenRepository phanQuyenRepository, IChucVuRepository chucVuRepository)
        {
            _phanQuyenRepository = phanQuyenRepository;
            _chucVuRepository = chucVuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhanQuyen()
        {
            var phanQuyens = await _phanQuyenRepository.GetAllPhanQuyen();
            var phanquyenDTO = phanQuyens.Select(c=>c.ToPhanQuyenDTO());
            return Ok(phanquyenDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhanQuyenById(int id)
        {
            var phanQuyen = await _phanQuyenRepository.GetPhanQuyenById(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            return Ok(phanQuyen.ToPhanQuyenDTO());
        }
        [HttpPost]
        [Route("{chucvuId}")]
        public async Task<IActionResult> CreatePhanQuyen([FromRoute]int ChucVuId,[FromBody] CreatePhanQuyenDTO phanQuyenDTO)
        {
            if(!await _chucVuRepository.ChucVuExists(ChucVuId))
            {
                return BadRequest("Chức vụ không tồn tại");
            }
            var phanQuyen = phanQuyenDTO.ToPhanQuyenFromCreateDTO(ChucVuId);
            await _phanQuyenRepository.CreatePhanQuyen(phanQuyen);
            return CreatedAtAction(nameof(GetPhanQuyenById), new {id = phanQuyen}, phanQuyen.ToPhanQuyenDTO());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePhanQuyen([FromRoute] int id, [FromBody] UpdatePhanQuyenDTO phanQuyenDTO)
        {
            var phanQuyen = await _phanQuyenRepository.GetPhanQuyenById(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            phanQuyen = phanQuyenDTO.ToPhanQuyenFromUpdateDTO();
            await _phanQuyenRepository.UpdatePhanQuyen(id,phanQuyen);
            return Ok(phanQuyen.ToPhanQuyenDTO());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePhanQuyen([FromRoute] int id)
        {
            var phanQuyen = await _phanQuyenRepository.DeletePhanQuyen(id);
            if (phanQuyen == null)
            {
                return NotFound("Không tìm thấy phân quyền");
            }
            else
            {
                return Ok(phanQuyen.ToPhanQuyenDTO());
            }
        }

    }
}