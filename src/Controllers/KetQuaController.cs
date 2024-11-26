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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateKetQuaDTO updateKetQuaDTO)
        {
            var ketQuaToUpdate = await _context.KetQuas.FindAsync(id);
            if (ketQuaToUpdate == null)
                return NotFound();

            ketQuaToUpdate.Diem = updateKetQuaDTO.Diem;
            ketQuaToUpdate.SinhVienId = updateKetQuaDTO.SinhVienId;
            ketQuaToUpdate.CauHoiId = updateKetQuaDTO.CauHoiId;
            
            await _context.SaveChangesAsync();
            var studentDTO = ketQuaToUpdate.ToKetQuaDTO();
            return Ok(studentDTO);
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
