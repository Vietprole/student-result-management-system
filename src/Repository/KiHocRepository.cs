using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.KiHoc;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Repository
{
    public class KiHocRepository : IKiHocRepository
    {
        private readonly ApplicationDBContext _context;
        public KiHocRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ViewKiHocDTO> AddKiHocDTO(NewKiHocDTO newKiHocDTO)
        {
            var kiHoc = newKiHocDTO.toKiHocFromNewDTO();
            await _context.KiHocs.AddAsync(kiHoc);
            await _context.SaveChangesAsync();
            return kiHoc.toDTOFromKiHoc();

        }
        public bool CheckTenKiHoc(string tenKiHoc)
        {
            if (string.IsNullOrEmpty(tenKiHoc))
            {
                return false;   
            }
            // Kiểm tra chuỗi có bằng đúng một trong các cụm từ yêu cầu
            return tenKiHoc == "Kì 1" || tenKiHoc == "Kì 2" || tenKiHoc == "Kì hè";
        }

        public bool CheckNamHoc(string namHoc)
        {
            if (string.IsNullOrEmpty(namHoc))
            {
                return false;
            }
            var parts = namHoc.Split('-'); //Tách chuỗi
            if (parts.Length != 2)
                return false;


            if (int.TryParse(parts[0], out int startYear) && int.TryParse(parts[1], out int endYear)) //Kiểm tra xem có phải số không
            {
                return endYear - startYear == 1; //Kiểm tra xem năm kết thúc có phải năm bắt đầu + 1 không
            }

            return false;
        }


        public async Task<bool> DeleteKiHocDTO(int id)
        {
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return false;
            }
            _context.KiHocs.Remove(kiHoc);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ViewKiHocDTO>> GetAllKiHocDTO()
        {
            var kiHocs = await _context.KiHocs.ToListAsync();
            return kiHocs.Select(kiHoc => new ViewKiHocDTO
            {
                Id = kiHoc.Id,
                Ten = kiHoc.Ten,
                NamHoc = kiHoc.NamHoc
            }).ToList();
        }

        public async Task<ViewKiHocDTO?> GetKiHocDTO(int id)
        {
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return null;
            }
            return kiHoc.toDTOFromKiHoc();
        }


        public async Task<ViewKiHocDTO?> UpdateKiHocDTO(int id, NewKiHocDTO newKiHocDTO)
        {
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return null;
            }
            kiHoc.Ten = newKiHocDTO.Ten;
            kiHoc.NamHoc = newKiHocDTO.NamHoc;
            await _context.SaveChangesAsync();
            return kiHoc.toDTOFromKiHoc();
        }

        public async Task<bool> DuocSuaDiem(int id)
        {
            DateTime now = DateTime.Now;
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return false;
            }
            if(kiHoc.HanSuaDiem.HasValue && DateOnly.FromDateTime(kiHoc.HanSuaDiem.Value) < DateOnly.FromDateTime(now))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateHanSuaDiem(int id, DateTime hanSuaDiem)
        {
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return false;
            }
            kiHoc.HanSuaDiem = hanSuaDiem;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHanSuaCongThucDiem(int id, DateTime hanSuaCongThucDiem)
        {
            var kiHoc = await _context.KiHocs.FindAsync(id);
            if (kiHoc == null)
            {
                return false;
            }
            kiHoc.HanSuaCongThucDiem = hanSuaCongThucDiem;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}