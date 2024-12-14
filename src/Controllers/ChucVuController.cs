using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/chucvu")]
    [ApiController]
    // [Authorize]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _chucVuService;
        public ChucVuController(IChucVuService chucVuService)
        {
            _chucVuService = chucVuService;
        }
        [HttpGet("getlistchucvu")]
        public async Task<IActionResult> GetListChucVu()
        {
            try
            {
                var rs = await _chucVuService.GetListChucVu();
                return Ok(rs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}