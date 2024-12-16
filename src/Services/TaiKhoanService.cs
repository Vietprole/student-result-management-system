using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using System.Text.RegularExpressions;

namespace Student_Result_Management_System.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ApplicationDBContext _context;
        private readonly IChucVuService _chucVuService;
        private readonly IKhoaService _khoaService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;
        public TaiKhoanService(ApplicationDBContext context, IChucVuService chucVuService, IKhoaService khoaService, ITokenService tokenService,IPasswordHashService passwordHashService)
        {
            _context = context;
            _chucVuService = chucVuService;
            _khoaService = khoaService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
        }

        public async Task<string> CheckUsername(string username)
        {
            if (!IsValidUsername(username))
            {
                return "Username không hợp lệ";
            }
            var exits = await _context.TaiKhoans.FirstOrDefaultAsync(x=>x.Username.ToLower()==username.ToLower());
            if (exits != null)
            {
                return "Username đã tồn tại";
            }

            return "Username hợp lệ";
        }
        private static bool IsValidPassword(string password) //Kiểm tra password có hợp lệ không
        {
            string pattern = @"^(?=.*[A-Z])(?=.*[\W_])(?=.*\S).{6,}$";
            return Regex.IsMatch(password, pattern);
        }
        private static bool IsValidUsername(string username) //Kiểm tra username có hợp lệ không
        {
            string pattern = @"^[a-zA-Z0-9]{6,}$";
            return Regex.IsMatch(username, pattern);
        }

        public async Task<NewTaiKhoanDTO?> CreateTaiKhoan(CreateTaiKhoanDTO username)
        {
            TaiKhoan taiKhoan = username.ToTaiKhoanFromCreateTaiKhoanDTO();
            taiKhoan.Password = _passwordHashService.HashPassword(username.Password);
            
            var chucVu = await _chucVuService.GetIdChucVuByTen(username.TenChucVu);
            if (chucVu == null)
            {
                return null;
            }
            if(chucVu.TenChucVu == "TruongKhoa")
            {
                if (username.KhoaId == null && username.KhoaId <= 0)
                {
                    return null;
                }
            }
            var khoa = await _khoaService.GetKhoaByIdAsync(username.KhoaId??0);
            taiKhoan.ChucVuId = chucVu.Id;
            taiKhoan.ChucVu = chucVu;
            await _context.TaiKhoans.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();
            await _khoaService.UpdateTruongKhoa(username.KhoaId??0, taiKhoan);
            string token = await _tokenService.CreateToken(taiKhoan);
            return new NewTaiKhoanDTO
            {
                Username = taiKhoan.Username,
                Token = token
            };

        }

        public string CheckPassword(string password)
        {
            if(!IsValidPassword(password))
            {
                return "Password không hợp lệ";
            }
            return "Password hợp lệ";
        }

        public async Task<NewTaiKhoanDTO?> Login(TaiKhoanLoginDTO taiKhoanLoginDTO)
        {
            var taiKhoan = await _context.TaiKhoans.Include(c=>c.ChucVu).FirstOrDefaultAsync(x => x.Username == taiKhoanLoginDTO.TenDangNhap);
            if (taiKhoan == null)
            {
                return null;
            }
            if(!_passwordHashService.VerifyPassword(taiKhoanLoginDTO.MatKhau, taiKhoan.Password))
            {
                return null;
            }
            string token = await _tokenService.CreateToken(taiKhoan);
            return new NewTaiKhoanDTO
            {
                Username = taiKhoan.Username,
                Token = token
            };
        }

        public async Task<bool> DeleteTaiKhoan(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return false;
            }
            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaiKhoanDTO?> CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien)
        {
            TaiKhoan taiKhoan = taikhoanSinhVien.ToTaiKhoanFromCreateTaiKhoanDTO();
            if(await CheckUsername(taiKhoan.Username) != "Username hợp lệ")
            {
                return null;
            }
            taiKhoan.Password = _passwordHashService.HashPassword(taikhoanSinhVien.Password);
            var chucVu = await _chucVuService.GetIdChucVuByTen(taikhoanSinhVien.TenChucVu);
            if (chucVu == null)
            {
                return null;
            }
            taiKhoan.ChucVuId = chucVu.Id;
            taiKhoan.ChucVu = chucVu;
            await _context.TaiKhoans.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan.ToTaiKhoanDTO();
        }

        public async Task<TaiKhoan?> GetTaiKhoanById(int id)
        {
            return await _context.TaiKhoans.FindAsync(id);
        }
    }
}
