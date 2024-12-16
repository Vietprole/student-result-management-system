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

        private async Task<bool> IsMaNganhExisted(string maNganh)
        {
            var nganh = await _context.Nganhs.FirstOrDefaultAsync(n => n.MaNganh == maNganh);
            return nganh != null;
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
            if (await IsMaNganhExisted(nganh.MaNganh))
            {
                throw new BusinessLogicException("Mã ngành đã tồn tại");
            }
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();
            return await GetNganhByIdAsync(nganh.Id);
        }

        public async Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ?? 
                throw new NotFoundException("Không tìm thấy Ngành");

            if (updateNganhDTO.MaNganh != null && updateNganhDTO.MaNganh != nganh.MaNganh && 
                await IsMaNganhExisted(updateNganhDTO.MaNganh))
            {
                throw new BusinessLogicException("Mã ngành đã tồn tại");
            }

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
    }
}