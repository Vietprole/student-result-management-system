using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class ChucVuRepository : IChucVuRepository
    {
        private readonly ApplicationDBContext _context;
        public ChucVuRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ChucVuExists(int id)
        {
            return await _context.ChucVus.AnyAsync(x => x.Id == id);
        }

        public async Task<ChucVu> CreateChucVu(ChucVu chucVu)
        {
            await _context.ChucVus.AddAsync(chucVu);
            await _context.SaveChangesAsync();
            return chucVu;
        }

        public async Task<ChucVu?> DeleteChucVu(int id)
        {
            var chucVuModel = await _context.ChucVus.FirstOrDefaultAsync(x => x.Id == id);
            if (chucVuModel == null)
            {
                return null;
            }
            _context.ChucVus.Remove(chucVuModel);
            await _context.SaveChangesAsync();
            return chucVuModel;
        }

        public async Task<List<ChucVu>> GetAllChucVu()
        {
            return await _context.ChucVus.ToListAsync();
        }

        public async Task<ChucVu?> GetChucVuById(int id)
        {
            return await _context.ChucVus.FindAsync(id);
        }

        public Task<ChucVu?> GetChucVuByTenChucVu(string tenchucvu)
        {
            return _context.ChucVus.FirstOrDefaultAsync(x => x.TenChucVu == tenchucvu);
        }

        public async Task<ChucVu?> UpdateChucVu(int id,ChucVu chucVu)
        {
            var existingChucVu =  await _context.ChucVus.FindAsync(id);
            if (existingChucVu == null)
            {
                return null;
            }
            existingChucVu.TenChucVu = chucVu.TenChucVu;
            await _context.SaveChangesAsync();
            return existingChucVu;

        }
    }
}