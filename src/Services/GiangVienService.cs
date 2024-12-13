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

namespace Student_Result_Management_System.Services
{
    
    public class GiangVienService : IGiangVienService
    {
        private readonly ApplicationDBContext _context;
        private readonly IKhoaService _khoaService;
        private readonly ITaiKhoanService _taiKhoanService;
        public GiangVienService(ApplicationDBContext context, IKhoaService khoaService, ITaiKhoanService taiKhoanService)
        {
            _context = context;
            _khoaService = khoaService;
            _taiKhoanService = taiKhoanService;
        }
        public async Task<GiangVien?> CheckGiangVien(CreateGiangVienDTO giangVienDTO)
        {
            Khoa? khoa = await _khoaService.GetKhoaByIdAsync(giangVienDTO.KhoaId);
            if (khoa == null)
            {
                return null;
            }
            return new GiangVien
            {
                KhoaId = khoa.Id,
                Khoa = khoa
            };
        }

        public async Task<GiangVienDTO?> CreateGiangVien(CreateGiangVienDTO createGiangVienDTO)
        {
            TaiKhoanDTO? taiKhoanId = await CreateTaiKhoanGiangVien(createGiangVienDTO);
            if (taiKhoanId == null)
            {
                return null;
            }
            GiangVien giangVien = new GiangVien
            {
                TaiKhoanId = taiKhoanId.Id,
                TaiKhoan = await _taiKhoanService.GetTaiKhoanById(taiKhoanId.Id),
                KhoaId = createGiangVienDTO.KhoaId
            };
            _context.GiangViens.Add(giangVien);
            await _context.SaveChangesAsync();
            return giangVien.ToGiangVienDTO();
        }

        public async Task<TaiKhoanDTO?> CreateTaiKhoanGiangVien(CreateGiangVienDTO createGiangVienDTO)
        {
           string? MaKhoa = await  _khoaService.GetMaKhoa(createGiangVienDTO.KhoaId);
           if (MaKhoa == null)
           {
               return null;
           }
           int soluong = await GetCountGiangVien(createGiangVienDTO.KhoaId)+1;
           string Magiangvien = MaKhoa+(soluong + 1).ToString("D7");
           CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
           {
               Username = "gv"+Magiangvien,
               Password = "Gv@"+Magiangvien,
               TenChucVu = "GiangVien",
               HovaTen = createGiangVienDTO.Ten
           };
           TaiKhoanDTO? taiKhoanId = await _taiKhoanService.CreateTaiKhoanSinhVien(createTaiKhoanDTO);
           return taiKhoanId;
        }

        //public async Task<GiangVien?> DeleteGV(int id)
        //{
        //    var exits = await _context.GiangViens.Include(c=>c.TaiKhoan).FirstOrDefaultAsync(x=>x.Id==id);
        //    if(exits==null)
        //    {
        //        return null;
        //    }
        //    var taikhoan= await _taiKhoanService.DeleteUser(exits.TaiKhoan);
        //    _context.GiangViens.Remove(exits);
        //    return exits;
        //}

        public async Task<List<GiangVien>> GetAll(int[] id)
        {
            var giangViens =await _context.GiangViens.Include(c=>c.TaiKhoan).Where(x => id.Contains(x.Id)).ToListAsync();
            return giangViens;
        }

        public async Task<List<GiangVien>> GetAllByKhoaId(int khoaId)
        {
            List<GiangVien> giangViens = await _context.GiangViens.Include(x => x.TaiKhoan).Where(c=>c.KhoaId==khoaId).ToListAsync();
            return giangViens;
        }

        public async Task<List<GiangVien>> GetAllGiangVien()
        {
            List<GiangVien> giangViens = await _context.GiangViens.Include(c=>c.TaiKhoan).Include(gv=>gv.Khoa).ToListAsync();
            return giangViens;
        }
        

        public async Task<GiangVien?> GetById(int id)
        {
            return await _context.GiangViens.Include(x=>x.TaiKhoan).Include(gv=>gv.Khoa).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<int> GetCountGiangVien(int khoaId)
        {
            int count = await _context.GiangViens.CountAsync(x => x.KhoaId == khoaId);
            return count;
        }

        // public async Task<string> GetKhoaByIdAsync(string taikhoanId)
        // {
        //     var gv= await _context.GiangViens.FirstOrDefaultAsync(x=>x.TaiKhoanId==taikhoanId);
        //     if(gv==null)
        //     {
        //         return "";
        //     }
        //     return gv.KhoaId.ToString()??"";
        // }

        public async Task<GiangVien?> UpdateGV(int id, UpdateGiangVienDTO updateGiangVienDTO)
        {
            var exitsGV = await _context.GiangViens.Include(c=>c.TaiKhoan).Include(gv=>gv.Khoa).FirstOrDefaultAsync(x=>x.Id==id);
            if(exitsGV==null)
            {
                return null;
            }
            exitsGV.TaiKhoan.Ten=updateGiangVienDTO.Ten;
            exitsGV.KhoaId=updateGiangVienDTO.KhoaId;
            await _context.SaveChangesAsync();

            exitsGV = await _context.GiangViens.Include(c=>c.TaiKhoan).Include(gv=>gv.Khoa).FirstOrDefaultAsync(x=>x.Id==id);
            return exitsGV;
        }
    }
}