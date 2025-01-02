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
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class KhoaService : IKhoaService
    {
        private readonly ApplicationDBContext _context;
        public KhoaService(ApplicationDBContext context)
        {
            _context = context;
        }
        private async Task<bool> IsMaKhoaExisted(string maKhoa)
        {
            var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.MaKhoa == maKhoa);
            return khoa != null;
        }

        private async Task<bool> DoesKhoaHasRelatedEntities(int khoaId)
        {
            var khoa = await _context.Khoas
                .Include(k => k.Nganhs)
                .Include(k => k.GiangViens)
                .FirstOrDefaultAsync(k => k.Id == khoaId);

            return khoa?.Nganhs.Count != 0 || khoa?.GiangViens.Count != 0;
        }
        
        public async Task<Khoa> CreateKhoaAsync(Khoa khoa)
        {
            if (await IsMaKhoaExisted(khoa.MaKhoa))
            {
                throw new BusinessLogicException("Mã khoa đã tồn tại");
            }
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
            return khoa;
        }

        public async Task<string?> GetMaKhoa(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            return khoa?.MaKhoa;
        }

        public async Task<Khoa?> UpdateKhoaAsync(int id, UpdateKhoaDTO updateKhoaDTO)
        {
            var khoa = await _context.Khoas.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy Khoa");
            if (updateKhoaDTO.MaKhoa != null && updateKhoaDTO.MaKhoa != khoa.MaKhoa)
            {
                if (await IsMaKhoaExisted(updateKhoaDTO.MaKhoa))
                {
                    throw new BusinessLogicException("Mã khoa đã tồn tại");
                }
                if (await DoesKhoaHasRelatedEntities(id))
                {
                    throw new BusinessLogicException("Khoa chứa các đối tượng con, không thể thay đổi mã khoa");
                }
            }
            khoa = updateKhoaDTO.ToKhoaFromUpdateDTO(khoa);

            await _context.SaveChangesAsync();
            return khoa;
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

        public async Task<bool> DeleteKhoaAsync(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id) ?? throw new BusinessLogicException("Không tìm thấy Khoa");
            try
            {
                _context.Khoas.Remove(khoa);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new BusinessLogicException("Khoa chứa các đối tượng con, không thể xóa");
            }
            return true;
        }

        public async Task<bool> CheckCreateKhoa(CreateKhoaDTO createKhoaDTO)
        {
            var exitsTen = await _context.Khoas.FirstOrDefaultAsync(k => k.Ten == createKhoaDTO.Ten);
            var exitsMaKhoa = await _context.Khoas.FirstOrDefaultAsync(k => k.MaKhoa == createKhoaDTO.MaKhoa);
            if (exitsTen != null)
            {
                throw new BusinessLogicException("Tên khoa đã tồn tại");
            }
            if (exitsMaKhoa != null)
            {
                throw new BusinessLogicException("Mã khoa đã tồn tại");
            }
            return true;
        }
    }
}