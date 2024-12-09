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
                KhoaId = khoa.Id,
                Khoa = khoa
            };
        }

        public async Task<GiangVien?> CreateGiangVien(GiangVien giangVien,TaiKhoan taiKhoan)
        {
            giangVien.TaiKhoanId=taiKhoan.Id;
            giangVien.TaiKhoan=taiKhoan;
            await _context.GiangViens.AddAsync(giangVien);
            await _context.SaveChangesAsync();

            giangVien = await _context.GiangViens.Include(gv => gv.Khoa).FirstOrDefaultAsync(x => x.Id == giangVien.Id) ?? giangVien;
            return giangVien;
        }

        public async Task<TaiKhoan?> CreateTaiKhoanGiangVien(CreateGiangVienDTO createGiangVienDTO)
        {
            string? MaKhoa = await  _khoaRepository.GetMaKhoa(createGiangVienDTO.KhoaId);
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
            TaiKhoan? taiKhoanId = await _taiKhoanRepository.CreateTaiKhoanGiangVien(createTaiKhoanDTO);
            return taiKhoanId;
        }

        public async Task<GiangVien?> DeleteGV(int id)
        {
            var exits = await _context.GiangViens.Include(c=>c.TaiKhoan).FirstOrDefaultAsync(x=>x.Id==id);
            if(exits==null)
            {
                return null;
            }
            var taikhoan= await _taiKhoanRepository.DeleteUser(exits.TaiKhoan);
            _context.GiangViens.Remove(exits);
            return exits;
        }

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

        public async Task<string> GetKhoaId(string taikhoanId)
        {
            var gv= await _context.GiangViens.FirstOrDefaultAsync(x=>x.TaiKhoanId==taikhoanId);
            if(gv==null)
            {
                return "";
            }
            return gv.KhoaId.ToString()??"";
        }

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