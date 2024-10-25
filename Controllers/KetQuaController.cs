using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/ketqua")]
    [ApiController]
    public class KetQuaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public KetQuaController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<KetQua>>> GetAllKetQuas()
        {
            var ketQuas = await _context.KetQuas.ToListAsync();
            return Ok(ketQuas);
        }
        [HttpPost]
        public async Task<ActionResult<List<KetQua>>> AddKetQua([FromBody] List<KetQua> results)
        {
            if (results == null || results.Count == 0)
            {
                return BadRequest("The results list is empty or null.");
            }

            await _context.KetQuas.AddRangeAsync(results);
            await _context.SaveChangesAsync();
            return Ok(await _context.KetQuas.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<KetQua>>> CalculateCLO(int id)
        {
            var ketQuas = await _context.KetQuas.Where(kq => kq.SinhVienId == id).ToListAsync();
            if (ketQuas == null || ketQuas.Count == 0)
            {
                return NotFound("No result found for this student.");
            }

            var cloResults = new List<KetQua>();
            foreach (var clo in await _context.CLOs.ToListAsync())
            {
                var cloResult = new KetQua
                {
                    SinhVienId = id,
                    CLOId = clo.Id,
                    Diem = 0
                };
                foreach (var ketQua in ketQuas)
                {
                    if (ketQua.CLOId == clo.Id)
                    {
                        cloResult.Diem += ketQua.Diem;
                    }
                }
                cloResults.Add(cloResult);
            }

            return Ok(cloResults);
        }
    }
}
