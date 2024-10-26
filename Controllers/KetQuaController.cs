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

        [HttpPut("{studentId}/{LopHocPhanId}")]
        public async Task<ActionResult<KetQua>> UpdateKetQuaForSinhVienInLopHocPhan(int studentId, int LopHocPhanId, [FromBody] List<KetQua> ketQuas)
        {
            var resultToUpdate = await _context.KetQuas
                .FirstOrDefaultAsync(kq => kq.SinhVienId == studentId && kq.LopHocPhanId == LopHocPhanId);
            if (resultToUpdate == null)
            {
                return NotFound("KetQua not found.");
            }

            resultToUpdate.Diem = result.Diem;
            await _context.SaveChangesAsync();
            return Ok(resultToUpdate);
        }

        [HttpGet("calculate-diem-clo")]
        public async Task<ActionResult<double>> CalculateDiemCLO([FromQuery] int SinhVienId, [FromQuery] int CLOId)
        {
            // Get the CLO with the specified CLOId
            var clo = await _context.CLOs
                .Include(c => c.CauHois)
                .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(c => c.Id == CLOId);

            if (clo == null)
            {
                return NotFound("CLO not found.");
            }

            // Get the KetQua records for the specified SinhVienId and CauHoiIds from the CLO
            var cauHoiIds = clo.CauHois.Select(ch => ch.Id).ToList();
            var ketQuas = await _context.KetQuas
                .Where(kq => kq.SinhVienId == SinhVienId && cauHoiIds.Contains(kq.CauHoiId))
                .ToListAsync();

            if (ketQuas == null || ketQuas.Count == 0)
            {
                return NotFound("No KetQua records found for the specified SinhVienId and CLOId.");
            }

            // Calculate the results
            var results = new List<double>();
            foreach (var ketQua in ketQuas)
            {
                var cauHoi = clo.CauHois.FirstOrDefault(ch => ch.Id == ketQua.CauHoiId);
                if (cauHoi != null)
                {
                    var trongSo = cauHoi.BaiKiemTra.TrongSo;
                    var result = ketQua.Diem * trongSo;
                    results.Add(result);
                }
            }
            var total = results.Sum();
            return Ok(total);
        }
    }
}
