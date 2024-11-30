using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/ketqua")]
    [ApiController]
    [Authorize]
    public class KetQuaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public KetQuaController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        // IActionResult return any value type
        // public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<IActionResult> GetAll() // async go with Task<> to make function asynchronous
        {
            var ketQuas = await _context.KetQuas.ToListAsync();
            var ketQuaDTOs = ketQuas.Select(sv => sv.ToKetQuaDTO()).ToList();
            return Ok(ketQuaDTOs);
        }

        [HttpGet("{id}")]
        // Get single entry
        public async Task<IActionResult> GetById([FromRoute] int id) // async go with Task<> to make function asynchronous
        {
            var student = await _context.KetQuas.FindAsync(id);
            if (student == null)
                return NotFound();
            var studentDTO = student.ToKetQuaDTO();
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKetQuaDTO createKetQuaDTO)
        {
            var ketQua = createKetQuaDTO.ToKetQuaFromCreateDTO();
            await _context.KetQuas.AddAsync(ketQua);
            await _context.SaveChangesAsync();
            var ketQuaDTO = ketQua.ToKetQuaDTO();
            return CreatedAtAction(nameof(GetById), new { id = ketQua.Id }, ketQuaDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateKetQuaDTO updateKetQuaDTO)
        {
            // Validate SinhVien exists
            var sinhVien = await _context.SinhViens.FindAsync(updateKetQuaDTO.SinhVienId);
            if (sinhVien == null)
                return NotFound($"SinhVien not found with ID: {updateKetQuaDTO.SinhVienId}");

            // Validate CauHoi exists
            var cauHoi = await _context.CauHois.FindAsync(updateKetQuaDTO.CauHoiId);
            if (cauHoi == null)
                return NotFound($"CauHoi not found with ID: {updateKetQuaDTO.CauHoiId}");

            // Find existing KetQua
            var ketQuaToUpdate = await _context.KetQuas
                .FirstOrDefaultAsync(k => 
                    k.SinhVienId == updateKetQuaDTO.SinhVienId && 
                    k.CauHoiId == updateKetQuaDTO.CauHoiId);

            if (ketQuaToUpdate == null)
            {
                // Create new KetQua
                ketQuaToUpdate = new KetQua
                {
                    SinhVienId = updateKetQuaDTO.SinhVienId,
                    CauHoiId = updateKetQuaDTO.CauHoiId,
                    Diem = updateKetQuaDTO.Diem
                };
                await _context.KetQuas.AddAsync(ketQuaToUpdate);
            }
            else
            {
                // Update existing
                ketQuaToUpdate.Diem = updateKetQuaDTO.Diem;
            }

            await _context.SaveChangesAsync();
            return Ok(ketQuaToUpdate.ToKetQuaDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var ketQuaToDelete = await _context.KetQuas.FindAsync(id);
            if (ketQuaToDelete == null)
                return NotFound();
            _context.KetQuas.Remove(ketQuaToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("calculate-diem-clo")]
        public async Task<IActionResult> CalculateDiemCLO([FromQuery] int SinhVienId, [FromQuery] int CLOId)
        {
            var clo = await _context.CLOs
                .Include(c => c.CauHois)
                    .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(c => c.Id == CLOId);

            if (clo == null)
            {
                return NotFound("CLO not found.");
            }

            // Get all relevant CauHoi IDs for this CLO
            var cauHoiIds = clo.CauHois.Select(ch => ch.Id).ToList();

            // Get KetQua records and calculate weighted sum
            var ketQuas = await _context.KetQuas
                .Where(kq => kq.SinhVienId == SinhVienId && cauHoiIds.Contains(kq.CauHoiId))
                .ToListAsync();

            decimal totalScore = 0;
            foreach (var ketQua in ketQuas)
            {
                var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                totalScore += ketQua.Diem * cauHoi.BaiKiemTra.TrongSo;
            }

            return Ok(decimal.Round(totalScore, 2, MidpointRounding.AwayFromZero));
        }

        [HttpGet("calculate-diem-clo-max")]
        public async Task<IActionResult> CalculateDiemCLOMax([FromQuery] int CLOId)
        {
            var clo = await _context.CLOs
                .Include(c => c.CauHois)
                    .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(c => c.Id == CLOId);

            if (clo == null)
            {
                return NotFound("CLO not found.");
            }

            decimal maxScore = 0;
            foreach (var cauHoi in clo.CauHois)
            {
                maxScore += 10m * cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo;
            }

            return Ok(decimal.Round(maxScore, 2, MidpointRounding.AwayFromZero));
        }
    }
}
