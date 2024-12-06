using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly ApplicationDBContext _context;
        public KhoaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> CheckKhoa(string tenKhoa)
        {
            var khoa= await _context.Khoas.FirstOrDefaultAsync(x => x.Ten.ToLower() == tenKhoa.ToLower() || x.VietTat.ToLower() == tenKhoa.ToLower());
            if (khoa == null)
            {
                return 0;
            }
            return khoa.Id;
        }

        public async Task<Khoa?> CreateKhoa(Khoa khoa)
        {
            await _context.Khoas.AddAsync(khoa);
            await _context.SaveChangesAsync();
            return khoa;
        }

        public async Task<Khoa?> GetKhoaId(int id)
        {
            return await _context.Khoas.FindAsync(id);
        }

        public async Task<List<Khoa>> GetListKhoa()
        {
            List<Khoa> list_khoa = await _context.Khoas.ToListAsync();
            return list_khoa;
        }

        public async Task<string?> GetMaKhoa(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return null;
            }
            return khoa.MaKhoa;
        }
    }
}