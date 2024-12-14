using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Utils;

namespace ketQua_Result_Management_System.Controllers
{
    [Route("api/ketqua")]
    [ApiController]
    // [Authorize]
    public class KetQuaController : ControllerBase
    {
        private readonly IKetQuaService _ketQuaService;
        public KetQuaController(IKetQuaService ketQuaService)
        {
            _ketQuaService = ketQuaService;
        }

        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var ketQuaDTOs = await _ketQuaService.GetAllKetQuasAsync();
            return Ok(ketQuaDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var ketQuaDTO = await _ketQuaService.GetKetQuaByIdAsync(id);
            if (ketQuaDTO == null)
                return NotFound();

            return Ok(ketQuaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKetQuaDTO createKetQuaDTO)
        {
            var ketQuaDTO = await _ketQuaService.CreateKetQuaAsync(createKetQuaDTO);
            return CreatedAtAction(nameof(GetById), new { id = ketQuaDTO.Id }, ketQuaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateKetQuaDTO updateKetQuaDTO)
        {
            var updatedKetQuaDTO = await _ketQuaService.UpdateKetQuaAsync(id, updateKetQuaDTO);
            return Ok(updatedKetQuaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _ketQuaService.DeleteKetQuaAsync(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("calculate-diem-clo")]
        public async Task<IActionResult> CalculateDiemCLO([FromQuery] int SinhVienId, [FromQuery] int cLOId)
        {
            try
            {
                var diemCLO = await _ketQuaService.CalculateDiemCLO(SinhVienId, cLOId);
                return Ok(diemCLO);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("calculate-diem-clo-max")]
        public async Task<IActionResult> CalculateDiemCLOMax([FromQuery] int cLOId)
        {
            try {
                var diemCLOMax = await _ketQuaService.CalculateDiemCLOMax(cLOId);
                return Ok(diemCLOMax);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("calculate-diem-pk")]
        public async Task<IActionResult> CalculateDiemPk([FromQuery] int lopHocPhanId, [FromQuery] int sinhVienId, [FromQuery] int ploId)
        {
            try {
                var diemPk = await _ketQuaService.CalculateDiemPk(lopHocPhanId, sinhVienId, ploId);
                return Ok(diemPk);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
        [HttpGet("calculate-diem-plo")]
        public async Task<IActionResult> CalculateDiemPLO([FromQuery] int sinhVienId, [FromQuery] int ploId)
        {
            try {
                var diemPLO = await _ketQuaService.CalculateDiemPLO(sinhVienId, ploId);
                return Ok(diemPLO);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
    }
}
