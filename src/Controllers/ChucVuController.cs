using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/chucvu")]
    [ApiController]
    public class ChucVuController:ControllerBase
    {
        private readonly IChucVuRepository _chucVuRepository;
        public ChucVuController(IChucVuRepository chucVuRepository)
        {
            _chucVuRepository = chucVuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllChucVu()
        {
            var chucVu = await _chucVuRepository.GetAllChucVu();
            return Ok(chucVu.Select(chucVu => chucVu.toChucVuDTO()));
        }
        [HttpGet("tenchucvu")]
        public async Task<IActionResult> GetChucVuByTenChucVu([FromBody] CreateChucVuDTO tenchucvu)
        {
            var chucVu = await _chucVuRepository.GetChucVuByTenChucVu(tenchucvu.TenChucVu);
            if (chucVu == null)
            {
                return NotFound();
            }
            return Ok(chucVu.toChucVuDTO());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChucVuById(int id)
        {
            var chucVu = await _chucVuRepository.GetChucVuById(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            return Ok(chucVu);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChucVu([FromBody] CreateChucVuDTO chucVuDTO)
        {
            var chucvuModel= chucVuDTO.ToChucVuFromCreateChucVuDTO();
            await _chucVuRepository.CreateChucVu(chucvuModel);
            return CreatedAtAction(nameof(GetChucVuById), new {id = chucvuModel}, chucvuModel.toChucVuDTO());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateChucVu([FromRoute] int id,[FromBody]CreateChucVuDTO chucVuDTO)
        {
            var chucvuModel = await _chucVuRepository.UpdateChucVu(id,chucVuDTO.ToChucVuFromCreateChucVuDTO());
            if (chucvuModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chucvuModel.toChucVuDTO());
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteChucVu([FromRoute] int id)
        {
            var chucvuModel = await _chucVuRepository.DeleteChucVu(id);
            if(chucvuModel == null)
            {
                return NotFound();
            }
            return Ok(chucvuModel.toChucVuDTO());
        }
    }
}