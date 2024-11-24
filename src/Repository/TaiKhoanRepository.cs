using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly UserManager<TaiKhoan> _userManager;
        private readonly IChucVuRepository _chucVuRepository;
        private readonly SignInManager<TaiKhoan> _signInManager;
        public TaiKhoanRepository(UserManager<TaiKhoan> userManager, IChucVuRepository chucVuRepository, SignInManager<TaiKhoan> signInManager)
        {
            _userManager = userManager;
            _chucVuRepository = chucVuRepository;
            _signInManager = signInManager;
        }

        public Task<bool> CheckPassword(TaiKhoan taikhoan, string password)
        {
            var result = _userManager.CheckPasswordAsync(taikhoan, password);
            return result;
        }

        public async Task<TaiKhoan?> CheckUser(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task<TaiKhoan?> CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien)
        {
            var user = taikhoanSinhVien.ToTaiKhoanFromCreateTaiKhoanDTO();
                if (string.IsNullOrEmpty(taikhoanSinhVien.TenChucVu))
                {
                    return null;
                }
                var role_result = await _chucVuRepository.GetIdChucVu(taikhoanSinhVien.TenChucVu);
                if (role_result != null)
                {
                    if (string.IsNullOrEmpty(taikhoanSinhVien.Password))
                    {
                        return null;
                    }
                    var result = await _userManager.CreateAsync(user, taikhoanSinhVien.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, taikhoanSinhVien.TenChucVu);
                        return result.Succeeded ? user : null;
                    }
                    return null;
                }
                return null;
        }

        public async Task<TaiKhoan?> CreateUser(CreateTaiKhoanDTO createTaiKhoanDTO,ChucVuDTO chucVuDTO)
        {
            try
            {
                var user = createTaiKhoanDTO.ToTaiKhoanFromCreateTaiKhoanDTO();
                if (string.IsNullOrEmpty(chucVuDTO.TenChucVu))
                {
                    return null;
                }
                var role_result = await _chucVuRepository.GetIdChucVu(chucVuDTO.TenChucVu);
                if (role_result != null)
                {
                    if (string.IsNullOrEmpty(createTaiKhoanDTO.Password))
                    {
                        return null;
                    }
                    var result = await _userManager.CreateAsync(user, createTaiKhoanDTO.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, chucVuDTO.TenChucVu);
                        return user;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TaiKhoan?> DeleteUser(TaiKhoan taikhoan)
        {
            var result = await _userManager.DeleteAsync(taikhoan);
            if (result.Succeeded)
            {
                return taikhoan;
            }
            return null;
        }
    }
}