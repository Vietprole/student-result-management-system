using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/plo")]
    [ApiController]
    // [Authorize]
    public class PLOController : ControllerBase
    {
        private readonly IPLOService _ploService;
        private readonly ICLOService _cLOService;
        private readonly IHocPhanService _hocPhanService;
        public PLOController(IPLOService ploService, ICLOService cLOService, IHocPhanService hocPhanService)
        {
            _ploService = ploService;
            _cLOService = cLOService;
            _hocPhanService = hocPhanService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(
           [FromQuery] int? nganhId,
           [FromQuery] int? lopHocPhanId)
        {
            if (lopHocPhanId.HasValue)
            {
                var plos = await _ploService.GetPLOsByLopHocPhanIdAsync(lopHocPhanId.Value);
                return Ok(plos);
            }
            
            if (nganhId.HasValue)
            {
                var plos = await _ploService.GetPLOsByNganhIdAsync(nganhId.Value);
                return Ok(plos);
            }

            var allPlos = await _ploService.GetAllPLOsAsync();
            return Ok(allPlos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var plo = await _ploService.GetPLOByIdAsync(id);
            if (plo == null)
                return NotFound("Không tìm thấy PLO");
            return Ok(plo);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,PhongDaoTao,NguoiPhuTrachCTĐT")]
        public async Task<IActionResult> Create([FromBody] CreatePLODTO createPLODTO)
        {
            var plo = await _ploService.CreatePLOAsync(createPLODTO);
            return CreatedAtAction(nameof(GetById), new { id = plo.Id }, plo);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,NguoiPhuTrachCTĐT")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePLODTO updatePLODTO)
        {
            var plo = await _ploService.UpdatePLOAsync(id, updatePLODTO);
            if (plo == null)
                return NotFound("Không tìm thấy PLO");
            return Ok(plo);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,PhongDaoTao,NguoiPhuTrachCTĐT")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _ploService.DeletePLOAsync(id);
            if (!result)
                return NotFound("Không tìm thấy PLO");
            return NoContent();
        }

        [HttpGet("{id}/clo")]
        [Authorize]
        public async Task<IActionResult> GetCLOs([FromRoute] int id)
        {
            var pLO = await _ploService.GetPLOByIdAsync(id);
            if (pLO == null)
                return NotFound("Không tìm thấy PLO");

            var cLODTOs = await _cLOService.GetCLOsByPLOIdAsync(id);
            return Ok(cLODTOs);
        }

            [HttpGet("{id}/hocphan")]
            [Authorize]
            public async Task<IActionResult> GetHocPhans([FromRoute] int id)
        {
            var pLO = await _ploService.GetPLOByIdAsync(id);
            if (pLO == null)
                return NotFound("Không tìm thấy PLO");

            var hocPhans = await _hocPhanService.GetFilteredHocPhansAsync(null, null, id, null, null);
            return Ok(hocPhans.Select(hp => hp.ToHocPhanDTO()));
        }

        // [HttpPost("{id}/add-clos")]
        // public async Task<IActionResult> AddCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        // {
        //     var pLO = await _ploService.AddCLOsToPLOAsync(id, cLOIds);
        //     if (pLO == null)
        //         return NotFound("PLO not found");

        //     return CreatedAtAction(nameof(GetCLOs), new { id = pLO.Id }, pLO.CLOs.Select(sv => sv.ToCLODTO()).ToList());
        // }

        [HttpPut("{id}/clo")]
        [Authorize(Roles = "Admin,PhongDaoTao,NguoiPhuTrachCTĐT")]
        public async Task<IActionResult> UpdateCLOs([FromRoute] int id, [FromBody] int[] cLOIds)
        {
            var pLO = await _ploService.GetPLOByIdAsync(id);
            if (pLO == null)
                return NotFound("Không tìm thấy PLO");

            try {
                var updatedCLOs = await _ploService.UpdateCLOsOfPLOAsync(id, cLOIds);
                return Ok(updatedCLOs);
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

        [HttpPut("{id}/hocphan")]
        [Authorize(Roles = "Admin,PhongDaoTao,NguoiPhuTrachCTĐT")]
        public async Task<IActionResult> UpdateHocPhans([FromRoute] int id, [FromBody] int[] hocPhanIds)
        {
            var pLO = await _ploService.GetPLOByIdAsync(id);
            if (pLO == null)
                return NotFound("Không tìm thấy PLO");

            try {
                var updatedHocPhans = await _ploService.UpdateHocPhansOfPLOAsync(id, hocPhanIds);
                return Ok(updatedHocPhans);
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpDelete("{id}/remove-clo/{cLOId}")]
        // public async Task<IActionResult> RemoveCLO([FromRoute] int id, [FromRoute] int cLOId)
        // {
        //     var pLO = await _ploService.RemoveCLOFromPLOAsync(id, cLOId);
        //     if (pLO == null)
        //         return NotFound("PLO not found");

        //     return CreatedAtAction(nameof(GetCLOs), new { id = pLO.Id }, pLO.CLOs.Select(sv => sv.ToCLODTO()).ToList());
        // }
    }
}
