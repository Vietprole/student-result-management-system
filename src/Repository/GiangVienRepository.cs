using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IKhoaRepository _khoaRepository;
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        public GiangVienRepository(ApplicationDBContext context, IKhoaRepository khoaRepository,ITaiKhoanRepository taiKhoanRepository)
        {
            _context = context;
            _khoaRepository = khoaRepository;
            _taiKhoanRepository = taiKhoanRepository;
        }
        public async Task<GiangVien?> CheckGiangVien(CreateGiangVienDTO giangVienDTO)
        {
            Khoa? khoa = await _khoaRepository.GetKhoaId(giangVienDTO.KhoaId);
            if (khoa == null)
            {
                return null;
            }
            return new GiangVien
            {
                Ten = giangVienDTO.Ten,
                KhoaId = khoa.Id,
                Khoa = khoa
            };
        }

        public async Task<GiangVien?> CreateGiangVien(GiangVien giangVien)
        {
            string? MaKhoa = await  _khoaRepository.GetMaKhoa(giangVien.KhoaId??0);
            if (MaKhoa == null)
            {
                return null;
            }
            int soluong = await GetCountGiangVien(giangVien.KhoaId??0)+1;
            string Magiangvien = MaKhoa+(soluong + 1).ToString("D7");
            CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
            {
                Username = "gv"+Magiangvien,
                Password = "Gv@"+Magiangvien,
                TenChucVu = "GiangVien"
            };
            TaiKhoan? taiKhoanId = await _taiKhoanRepository.CreateTaiKhoanSinhVien(createTaiKhoanDTO);
            if (taiKhoanId == null)
            {
                return null;
            }
            giangVien.TaiKhoanId = taiKhoanId.Id;
            giangVien.TaiKhoan = taiKhoanId;
            await _context.GiangViens.AddAsync(giangVien);
            await _context.SaveChangesAsync();
            return giangVien;
        }

        public async Task<int> GetCountGiangVien(int khoaId)
        {
            int count = await _context.GiangViens.CountAsync(x => x.KhoaId == khoaId);
            return count;
        }
    }
}