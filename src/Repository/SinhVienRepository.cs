using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IKhoaRepository _khoaRepository;
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        public SinhVienRepository(ApplicationDBContext context, IKhoaRepository khoaRepository,ITaiKhoanRepository taiKhoanRepository)
        {
            _context = context;
            _khoaRepository = khoaRepository;
            _taiKhoanRepository = taiKhoanRepository;
        }
        public async Task<SinhVien?> CheckSinhVien(CreateSinhVienDTO sinhVienDTO)
        {
            int khoaId = await _khoaRepository.CheckKhoa(sinhVienDTO.TenKhoa);
            if (khoaId == 0)
            {
                return null;
            }
            if(sinhVienDTO.NamBatDau>=DateTime.Now.Year && sinhVienDTO.NamBatDau<2010)
            {
                return null;
            }
            SinhVien sinhVien = new SinhVien
            {
                Ten = sinhVienDTO.Ten,
                KhoaId = khoaId,
                NamBatDau = sinhVienDTO.NamBatDau
            };
            return sinhVien;

        }

        public async Task<SinhVien?> CreateSinhVien(SinhVien sinhVien)
        {
            string? MaKhoa = await  _khoaRepository.GetMaKhoa(sinhVien.KhoaId??0);
            if (MaKhoa == null)
            {
                return null;
            }
            string NamBatDau = sinhVien.NamBatDau.ToString().Substring(sinhVien.NamBatDau.ToString().Length - 2);
            int soluong = await GetSinhVienByKhoa(sinhVien.KhoaId??0)+1;
            string MaSinhVien = MaKhoa + NamBatDau + (soluong + 1).ToString("D4");
            CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
            {
                Username = MaSinhVien,
                Password = "Sv@"+MaSinhVien,
                TenChucVu = "SinhVien"
            };
            TaiKhoan? taiKhoanId = await _taiKhoanRepository.CreateTaiKhoanSinhVien(createTaiKhoanDTO);
            if (taiKhoanId == null)
            {
                return null;
            }
            sinhVien.TaiKhoanId = taiKhoanId.Id;
            sinhVien.TaiKhoan = taiKhoanId;
            await _context.SinhViens.AddAsync(sinhVien);
            await _context.SaveChangesAsync();
            return sinhVien;

        }

        public async Task< string?> CreateTaiKhoanSinhVien(SinhVien sinhVien)
        {
            string? MaKhoa = await  _khoaRepository.GetMaKhoa(sinhVien.KhoaId??0);
            if (MaKhoa == null)
            {
                return null;
            }
            string NamBatDau = sinhVien.NamBatDau.ToString().Substring(sinhVien.NamBatDau.ToString().Length - 2);
            int soluong = await GetSinhVienByKhoa(sinhVien.KhoaId??0)+1;
            string MaSinhVien = MaKhoa + NamBatDau + (soluong + 1).ToString("D4");
            CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
            {
                Username = MaSinhVien,
                Password = "Sv@"+MaSinhVien,
                TenChucVu = "SinhVien"
            };
            TaiKhoan? taiKhoan = await _taiKhoanRepository.CreateUser(createTaiKhoanDTO,createTaiKhoanDTO.TenChucVu.ToChucVuDTOFromString());
            if (taiKhoan == null)
            {
                return null;
            }
            return taiKhoan.Id;
        }

        public async Task<int> GetSinhVienByKhoa(int khoaId)
        {
            int count = await _context.SinhViens.CountAsync(x => x.KhoaId == khoaId);
            return count;
        }
    }
}