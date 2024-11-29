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
    public class PhanQuyenRepository : IPhanQuyenRepository
    {
        private readonly ApplicationDBContext _context;
        public PhanQuyenRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PhanQuyen> CreatePhanQuyen(PhanQuyen phanQuyen)
        {
            await _context.PhanQuyens.AddAsync(phanQuyen);
            await _context.SaveChangesAsync();
            return phanQuyen;
        }

        public async Task<PhanQuyen?> DeletePhanQuyen(int id)
        {
            var phanQuyenModel = await _context.PhanQuyens.FirstOrDefaultAsync(x => x.Id == id);
            if (phanQuyenModel == null)
            {
                return null;
            }
            _context.PhanQuyens.Remove(phanQuyenModel);
            await _context.SaveChangesAsync();
            return phanQuyenModel;
        }

        public async Task<List<PhanQuyen>> GetAllPhanQuyen()
        {
            return await _context.PhanQuyens.ToListAsync();
        }

        public async Task<PhanQuyen?> GetPhanQuyenById(int id)
        {
            return await _context.PhanQuyens.FindAsync(id);
        }

        public async Task<PhanQuyen?> UpdatePhanQuyen(int id, PhanQuyen phanQuyen)
        {
            var existingPhanQuyen =await _context.PhanQuyens.FindAsync(id);
            if (existingPhanQuyen == null)
            {
                return null;
            }
            existingPhanQuyen.TenQuyen = phanQuyen.TenQuyen;
            existingPhanQuyen.ChucVuId = phanQuyen.ChucVuId;
            await _context.SaveChangesAsync();
            return existingPhanQuyen;

        }
    }
}