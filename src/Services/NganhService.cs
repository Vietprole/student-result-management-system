using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class NganhService : INganhService
    {
        private readonly ApplicationDBContext _context;
        public NganhService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateMaNganhAsync(int khoaId)
        {
            // Get MaKhoa
            var khoa = await _context.Khoas.FindAsync(khoaId) ?? throw new NotFoundException($"Không tìm thấy Khoa với Id: {khoaId}");
            string maKhoa = khoa.MaKhoa;

            // Get existing Nganh count for this Khoa
            int existingNganhCount = await _context.Nganhs
                .CountAsync(n => n.KhoaId == khoaId);

            // Calculate next sequential number
            int nextSequentialNumber = existingNganhCount + 1;
            if (nextSequentialNumber > 9999)
                throw new BusinessLogicException("Đã đạt đến số lượng Ngành tối đa cho Khoa này");

            // Combine MaKhoa and sequential number
            string maNganh = $"{maKhoa}{nextSequentialNumber:D4}";

            return maNganh;
        }
        public async Task<List<Nganh>> GetAllNganhsAsync()
        {
            return await _context.Nganhs.Include(n => n.Khoa).ToListAsync();
        }

        public async Task<List<Nganh>> GetNganhsByKhoaIdAsync(int khoaId)
        {
            return await _context.Nganhs
                .Include(n => n.Khoa)
                .Where(n => n.KhoaId == khoaId)
                .ToListAsync();
        }

        public async Task<Nganh?> GetNganhByIdAsync(int id)
        {
            return await _context.Nganhs
                .Include(n => n.Khoa)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
        public async Task<Nganh> CreateNganhAsync(Nganh nganh)
        {
            nganh.MaNganh = await GenerateMaNganhAsync(nganh.KhoaId);
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();
            return await GetNganhByIdAsync(nganh.Id) ?? throw new InvalidOperationException("Failed to retrieve the created Nganh.");
        }

        public async Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ?? 
                throw new NotFoundException("Không tìm thấy Ngành");

            nganh = updateNganhDTO.ToNganhFromUpdateDTO(nganh);
            await _context.SaveChangesAsync();
            return await GetNganhByIdAsync(id);
        }

        public async Task<bool> DeleteNganhAsync(int id)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ?? 
                throw new NotFoundException("Không tìm thấy Ngành");
            try
            {
                _context.Nganhs.Remove(nganh);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new BusinessLogicException("Ngành chứa các đối tượng con, không thể xóa");
            }
        }

        public async Task<List<HocPhan>> AddHocPhansToNganhAsync(int nganhId, int[] hocPhanIds)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId);
                if (hocPhan == null)
                {
                    // Nếu không tìm thấy học phần, có thể thêm một thông báo lỗi cụ thể
                    throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");
                }

                if (!nganh.HocPhans.Contains(hocPhan))
                    nganh.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            return nganh.HocPhans.ToList();
        }


        public async Task<List<HocPhan>> UpdateHocPhansOfNganhAsync(int nganhId, int[] hocPhanIds)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            nganh.HocPhans.Clear();
            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId) ?? throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");
                nganh.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            return nganh.HocPhans.ToList();
        }

        public async Task<bool> RemoveHocPhanFromNganhAsync(int nganhId, int hocPhanId)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            var hocPhan = await _context.HocPhans.FindAsync(hocPhanId) ?? throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");

            if (!nganh.HocPhans.Contains(hocPhan))
                return false;

            nganh.HocPhans.Remove(hocPhan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HocPhan>> GetHocPhansInNganhAsync(int nganhId)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");
            return nganh.HocPhans.ToList();
        }
    }
}