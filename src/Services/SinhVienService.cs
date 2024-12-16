using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Services
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ApplicationDBContext _context;
        private readonly IKhoaService _khoaService;
        private readonly ITaiKhoanService _taiKhoanService;
        public SinhVienService(ApplicationDBContext context, IKhoaService khoaService,ITaiKhoanService taiKhoanService)
        {
            _context = context;
            _khoaService = khoaService;
            _taiKhoanService = taiKhoanService;

        }
        public async Task<SinhVien?> CheckSinhVien(CreateSinhVienDTO sinhVienDTO)
        {
            Khoa? khoa = await _khoaService.GetKhoaByIdAsync(sinhVienDTO.KhoaId);
            if (khoa == null)
            {
                return null;
            }
            if(sinhVienDTO.NamNhapHoc>=DateTime.Now.Year && sinhVienDTO.NamNhapHoc<2010)
            {
                return null;
            }
            SinhVien sinhVien = new SinhVien
            {
                KhoaId = khoa.Id,
                Khoa = khoa,
                NamNhapHoc = sinhVienDTO.NamNhapHoc
            };
            return sinhVien;

        }

        public async Task<SinhVienDTO?> CreateSinhVien(CreateSinhVienDTO createSinhVienDTO)
        {
            TaiKhoanDTO? taiKhoan = await CreateTaiKhoanSinhVien(createSinhVienDTO);
            if (taiKhoan == null)
            {
                return null;
            }
            SinhVien sinhVien = new SinhVien
            {
                TaiKhoanId = taiKhoan.Id,
                TaiKhoan =await _taiKhoanService.GetTaiKhoanById(taiKhoan.Id),
                KhoaId = createSinhVienDTO.KhoaId,
                NamNhapHoc = createSinhVienDTO.NamNhapHoc,
                MaSinhVien = taiKhoan.Username
            };
            _context.SinhViens.Add(sinhVien);
            await _context.SaveChangesAsync();
            SinhVienDTO sinhVienDTO = sinhVien.ToSinhVienDTO();
            return sinhVienDTO;

        }

        public async Task<TaiKhoanDTO?> CreateTaiKhoanSinhVien(CreateSinhVienDTO taikhoanSinhVien)
        {
            string? MaKhoa = await  _khoaService.GetMaKhoa(taikhoanSinhVien.KhoaId);
           if (MaKhoa == null)
           {
               return null;
           }
           string NamNhapHoc = taikhoanSinhVien.NamNhapHoc.ToString().Substring(taikhoanSinhVien.NamNhapHoc.ToString().Length - 2);
           int soluong = await GetSinhVienByKhoa(taikhoanSinhVien.KhoaId)+1;
           while(true)
           {
               if(await _context.SinhViens.AnyAsync(sv => sv.MaSinhVien == MaKhoa + NamNhapHoc + (soluong + 1).ToString("D4")))
               {
                   soluong++;
               }
               else
               {
                   break;
               }
           }
           string MaSinhVien = MaKhoa + NamNhapHoc + (soluong + 1).ToString("D4");
           CreateTaiKhoanDTO createTaiKhoanDTO = new CreateTaiKhoanDTO
           {
               Username = MaSinhVien,
               Password = "Sv@"+MaSinhVien,
               TenChucVu = "SinhVien",
               HovaTen = taikhoanSinhVien.Ten
           };
           TaiKhoanDTO? taiKhoanId = await _taiKhoanService.CreateTaiKhoanSinhVien(createTaiKhoanDTO);
           return taiKhoanId;
        }

        public async Task<SinhVien?> DeleteSV(int id)
        {
           var exits = await _context.SinhViens.Include(c=>c.TaiKhoan).FirstOrDefaultAsync(x=>x.Id==id);
           if(exits==null)
           {
               return null;
           }
           if (exits.TaiKhoan != null)
           {
               var taikhoan = await _taiKhoanService.DeleteTaiKhoan(exits.TaiKhoan.Id);
           }
           _context.SinhViens.Remove(exits);
           await _context.SaveChangesAsync();
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
            if (exitsSV.TaiKhoan != null)
            {
                if (updateSinhVienDTO.Ten != null)
                {
                    exitsSV.TaiKhoan.Ten = updateSinhVienDTO.Ten;
                }
            }
            exitsSV.KhoaId= (int)updateSinhVienDTO.KhoaId;
            exitsSV.NamNhapHoc= (int)updateSinhVienDTO.NamNhapHoc;
            await _context.SaveChangesAsync();

            exitsSV = await _context.SinhViens.Include(c=>c.TaiKhoan).Include(sv=>sv.Khoa).FirstOrDefaultAsync(x=>x.Id==id);
            return exitsSV;
        }
    }
}