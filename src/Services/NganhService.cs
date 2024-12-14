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
            var nganh = await _context.Nganhs.FirstOrDefaultAsync(k => k.MaNganh == maNganh);
            return nganh != null;
        }
        public async Task<Nganh> CreateNganhAsync(Nganh nganh)
        {
            if (await IsMaNganhExisted(nganh.MaNganh))
            {
                throw new BusinessLogicException("Mã nganh đã tồn tại");
            }
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();
            return nganh;
        }

        public async Task<List<Nganh>> GetAllNganhsAsync()
        {
            var list_nganh = await _context.Nganhs.ToListAsync();
            return list_nganh;
        }
        public async Task<Nganh?> GetNganhByIdAsync(int id)
        {
            var nganh = await _context.Nganhs.FindAsync(id);
            // return nganh?.ToNganhDTO();
            return nganh;
        }

        public async Task<string?> GetMaNganh(int id)
        {
            var nganh = await _context.Nganhs.FindAsync(id);
            return nganh?.MaNganh;
        }

        public async Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy Nganh");
            if (updateNganhDTO.MaNganh != null && await IsMaNganhExisted(updateNganhDTO.MaNganh))
            {
                throw new BusinessLogicException("Mã nganh đã tồn tại");
            }
            nganh = updateNganhDTO.ToNganhFromUpdateDTO(nganh);

            await _context.SaveChangesAsync();
            return nganh;
        }

        public async Task<Nganh?> UpdateTruongNganh(int nganhid, TaiNganhn truongnganh)
        {
            var nganh = await _context.Nganhs.FindAsync(nganhid);
            if (nganh == null)
            {
                return null;
            }
            nganh.TruongNganhId = truongnganh.Id;
            nganh.TruongNganh = truongnganh;
            _context.Nganhs.Update(nganh);
            await _context.SaveChangesAsync();
            return nganh;
        }

        public async Task<bool> DeleteNganhAsync(int id)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ?? throw new BusinessLogicException("Không tìm thấy Nganh");
            try
            {
                _context.Nganhs.Remove(nganh);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new BusinessLogicException("Nganh chứa các đối tượng con, không thể xóa");
            }
            return true;
        }
    }
}