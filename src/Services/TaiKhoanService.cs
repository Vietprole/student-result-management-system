using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;
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
        private readonly ILogger<TaiKhoanService> _logger;
        public TaiKhoanService(ApplicationDBContext context, IChucVuService chucVuService, IKhoaService khoaService, ITokenService tokenService,IPasswordHashService passwordHashService, ILogger<TaiKhoanService> logger)
        {
            _context = context;
            _chucVuService = chucVuService;
            _khoaService = khoaService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
            _logger = logger;
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
            // password phải có ít nhất 6 ký tự, 1 ký tự viết hoa, 1 ký tự đặc biệt và không chứa khoảng trắng
            string pattern = @"^(?=.*[A-Z])(?=.*[\W_])[^\s]{6,}$";
            return Regex.IsMatch(password, pattern);
        }
        private static bool IsValidUsername(string username) //Kiểm tra username có hợp lệ không
        {
            // username phải có ít nhất 5 ký tự, max 100 ký tự, cho phép "_", không chứa ký tự đặc biệt và không chứa khoảng trắng
            string pattern = @"^[a-zA-Z0-9_]{5,100}$";
            return Regex.IsMatch(username, pattern);
        }

        public async Task<List<TaiKhoanDTO>> GetFilteredTaiKhoans(int? chucVuId)
        {
            var taiKhoans = await _context.TaiKhoans.Include(c => c.ChucVu).Where(x => chucVuId == null || x.ChucVuId == chucVuId).ToListAsync();
            return taiKhoans.Select(x => x.ToTaiKhoanDTO()).ToList();
        }

        public async Task<NewTaiKhoanDTO?> CreateTaiKhoan(CreateTaiKhoanDTO username)
        {
            TaiKhoan taiKhoan = username.ToTaiKhoanFromCreateTaiKhoanDTO();
            taiKhoan.Password = _passwordHashService.HashPassword(username.Password);
            
            var chucVu = await _context.ChucVus.FindAsync(username.ChucVuId);
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

        public async Task<TaiKhoanDTO?> UpdateTaiKhoan(int id, UpdateTaiKhoanDTO updateTaiKhoanDTO){
            var taiKhoan = await _context.TaiKhoans.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy tài khoản");
            if (!string.IsNullOrEmpty(updateTaiKhoanDTO.Username))
            {
                if (!IsValidUsername(updateTaiKhoanDTO.Username))
                {
                    throw new BusinessLogicException("Username không hợp lệ");
                }
            }
            taiKhoan = updateTaiKhoanDTO.ToTaiKhoanFromUpdateDTO(taiKhoan);
            if (!string.IsNullOrEmpty(updateTaiKhoanDTO.Password))
            {
                if (!IsValidPassword(updateTaiKhoanDTO.Password))
                {
                    throw new BusinessLogicException("Password không hợp lệ");
                }
                taiKhoan.Password = _passwordHashService.HashPassword(updateTaiKhoanDTO.Password);  
            }
            await _context.SaveChangesAsync();
            taiKhoan = await GetTaiKhoanById(id) ?? throw new NotFoundException("Không tìm thấy tài khoản");
            return taiKhoan.ToTaiKhoanDTO();
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
            var taiKhoan = _context.TaiKhoans.Include(c=>c.ChucVu).SingleOrDefault(x => x.Username == taiKhoanLoginDTO.TenDangNhap);
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
            var chucVu = _chucVuService.GetChucVuById(taikhoanSinhVien.ChucVuId);
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
            return await _context.TaiKhoans.Include(tk => tk.ChucVu).FirstOrDefaultAsync(tk => tk.Id == id);
        }

        public async Task<bool> ChangePassword(int id, ChangePasswordDTO changePasswordDTO)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy tài khoản");
            if (!_passwordHashService.VerifyPassword(changePasswordDTO.OldPassword, taiKhoan.Password))
            {
                throw new BusinessLogicException("Mật khẩu cũ không đúng");
            }
            taiKhoan.Password = _passwordHashService.HashPassword(changePasswordDTO.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetPassword(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy tài khoản");
            if (taiKhoan.ChucVuId == 2){//GiangVien
                taiKhoan.Password = _passwordHashService.HashPassword("Gv@" + taiKhoan.Username);
                await _context.SaveChangesAsync();
                return true;
            }
            
            if (taiKhoan.ChucVuId == 3){//SinhVien
                taiKhoan.Password = _passwordHashService.HashPassword("Sv@" + taiKhoan.Username);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Resetting password for SinhVien {Username} to {Password}", 
                taiKhoan.Username, 
                "Sv@" + taiKhoan.Username);
                return true;
            }
            
            taiKhoan.Password = _passwordHashService.HashPassword("Password@123456");
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetPasswordForSinhVienGiangVien(int id){
            var taiKhoan = await _context.TaiKhoans.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy tài khoản");
            if (taiKhoan.ChucVuId != 2 && taiKhoan.ChucVuId != 3){
                throw new BusinessLogicException("Tài khoản không phải là sinh viên hoặc giảng viên");
            }
            await ResetPassword(id);
            return true;
        }
    }
}
