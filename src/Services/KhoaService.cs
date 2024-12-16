using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Khoa;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Services
{
    public class KhoaService : IKhoaService
    {
        private readonly ApplicationDBContext _context;
        public KhoaService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> CheckCreateKhoa(CreateKhoaDTO createKhoaDTO)
        {
            var exits = await _context.Khoas.FirstOrDefaultAsync(x=>x.Ten.ToLower()==createKhoaDTO.Ten.ToLower());
            if (exits != null)
            {
                return "Khoa đã tồn tại";
            }
            var exitsMa= await _context.Khoas.FirstOrDefaultAsync(x=>x.MaKhoa.ToLower()==createKhoaDTO.MaKhoa.ToLower());
            if (exitsMa != null)
            {
                return "Mã khoa đã tồn tại";
            }
            return "Khoa hợp lệ";
        }

        public async Task<Khoa?> CreateKhoa(Khoa khoa)
        {
           await _context.Khoas.AddAsync(khoa);
           await _context.SaveChangesAsync();
           return khoa;
        }

        public async Task<List<Khoa>> GetAllKhoasAsync()
        {
            var list_khoa = await _context.Khoas.ToListAsync();
            return list_khoa;
        }
        public async Task<Khoa?> GetKhoaByIdAsync(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            // return khoa?.ToKhoaDTO();
            return khoa;
        }

        public async Task<string?> GetMaKhoa(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            return khoa?.MaKhoa;
        }

        public async Task<KhoaDTO?> UpdateKhoa(int id, UpdateKhoaDTO updateKhoaDTO)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return null;
            }
            khoa.Ten = updateKhoaDTO.Ten;
            khoa.MaKhoa = updateKhoaDTO.MaKhoa;
            _context.Khoas.Update(khoa);
            await _context.SaveChangesAsync();
            return khoa.ToKhoaDTO();
        }

        public async Task<Khoa?> UpdateTruongKhoa(int khoaid, TaiKhoan truongkhoa)
        {
            var khoa = await _context.Khoas.FindAsync(khoaid);
            if (khoa == null)
            {
                return null;
            }
            khoa.TruongKhoaId = truongkhoa.Id;
            khoa.TruongKhoa = truongkhoa;
            _context.Khoas.Update(khoa);
            await _context.SaveChangesAsync();
            return khoa;
        }
    }
}