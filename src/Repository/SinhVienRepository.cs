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
            Khoa? khoa = await _khoaRepository.GetKhoaId(sinhVienDTO.KhoaId);
            if (khoa == null)
            {
                return null;
            }
            if(sinhVienDTO.NamBatDau>=DateTime.Now.Year && sinhVienDTO.NamBatDau<2010)
            {
                return null;
            }
            SinhVien sinhVien = new SinhVien
            {
                KhoaId = khoa.Id,
                Khoa = khoa,
                NamBatDau = sinhVienDTO.NamBatDau
            };
            return sinhVien;

        }

        public async Task<SinhVien?> CreateSinhVien(SinhVien sinhvien,TaiKhoan taiKhoan)
        {
            sinhvien.TaiKhoanId=taiKhoan.Id;
            sinhvien.TaiKhoan=taiKhoan;
            await _context.SinhViens.AddAsync(sinhvien);
            await _context.SaveChangesAsync();

            sinhvien = await _context.SinhViens.Include(sv => sv.Khoa).FirstOrDefaultAsync(x => x.Id == sinhvien.Id) ?? sinhvien;
            return sinhvien;
        }

        public async Task<TaiKhoan?> CreateTaiKhoanSinhVien(CreateSinhVienDTO taikhoanSinhVien)
        {
             string? MaKhoa = await  _khoaRepository.GetMaKhoa(taikhoanSinhVien.KhoaId);
            if (MaKhoa == null)
            {
                return null;
            }
            string NamBatDau = taikhoanSinhVien.NamBatDau.ToString().Substring(taikhoanSinhVien.NamBatDau.ToString().Length - 2);
            int soluong = await GetSinhVienByKhoa(taikhoanSinhVien.KhoaId)+1;
            string MaSinhVien = MaKhoa + NamBatDau + (soluong + 1).ToString("D4");
            CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
            {
                Username = MaSinhVien,
                Password = "Sv@"+MaSinhVien,
                TenChucVu = "SinhVien",
                HovaTen = taikhoanSinhVien.Ten
            };
            TaiKhoan? taiKhoanId = await _taiKhoanRepository.CreateTaiKhoanSinhVien(createTaiKhoanDTO);
            return taiKhoanId;
        }

        public async Task<SinhVien?> DeleteSV(int id)
        {
            var exits = await _context.SinhViens.Include(c=>c.TaiKhoan).FirstOrDefaultAsync(x=>x.Id==id);
            if(exits==null)
            {
                return null;
            }
            var taikhoan= await _taiKhoanRepository.DeleteUser(exits.TaiKhoan);
            _context.SinhViens.Remove(exits);
            // await _context.SaveChangesAsync();
            return exits;
        }

        public async Task<List<SinhVien>> GetAll(int[] id)
        {
            List<SinhVien> sinhViens = await _context.SinhViens.Include(c=>c.TaiKhoan).Where(x=>id.Contains(x.Id)).ToListAsync();
            return sinhViens;
        }

        public async Task<List<SinhVien>> GetAllSinhVien()
        {
            List<SinhVien> sinhViens = await _context.SinhViens.Include(c=>c.TaiKhoan).Include(sv => sv.Khoa).ToListAsync();
            return sinhViens;
        }

        public async Task<SinhVien?> GetById(int id)
        {
            var sinhVien = await _context.SinhViens.Include(c => c.TaiKhoan).Include(sv=>sv.Khoa).FirstOrDefaultAsync(x => x.Id == id);
            return sinhVien;
        }

        public async Task<int> GetSinhVienByKhoa(int khoaId)
        {
            int count = await _context.SinhViens.CountAsync(x => x.KhoaId == khoaId);
            return count;
        }

        public async Task<SinhVien?> UpdateSV(int id, UpdateSinhVienDTO updateSinhVienDTO)
        {
            var exitsSV = await _context.SinhViens.Include(c=>c.TaiKhoan).FirstOrDefaultAsync(x=>x.Id==id);
            if(exitsSV==null)
            {
                return null;
            }
            exitsSV.TaiKhoan.HovaTen=updateSinhVienDTO.Ten;
            exitsSV.KhoaId=updateSinhVienDTO.KhoaId;
            exitsSV.NamBatDau=updateSinhVienDTO.NamBatDau;
            await _context.SaveChangesAsync();

            exitsSV = await _context.SinhViens.Include(c=>c.TaiKhoan).Include(sv=>sv.Khoa).FirstOrDefaultAsync(x=>x.Id==id);
            return exitsSV;
        }
    }
}