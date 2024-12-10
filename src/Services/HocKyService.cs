using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocKy;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Services
{
    public class HocKyService : IHocKyService
    {
        private readonly ApplicationDBContext _context;
        public HocKyService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<HocKyDTO> AddHocKyDTO(CreateHocKyDTO newHocKyDTO)
        {
            var hocKy = newHocKyDTO.toHocKyFromNewDTO();
            await _context.HocKies.AddAsync(hocKy);
            await _context.SaveChangesAsync();
            return hocKy.toDTOFromHocKy();

        }
        public bool CheckTenHocKy(string tenHocKy)
        {
            if (string.IsNullOrEmpty(tenHocKy))
            {
                return false;   
            }
            // Kiểm tra chuỗi có bằng đúng một trong các cụm từ yêu cầu
            return tenHocKy == "Kì 1" || tenHocKy == "Kì 2" || tenHocKy == "Kì hè";
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


        public async Task<bool> DeleteHocKyDTO(int id)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return false;
            }
            _context.HocKies.Remove(hocKy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HocKyDTO>> GetAllHocKyDTO()
        {
            var hocKies = await _context.HocKies.ToListAsync();
            return hocKies.Select(hocKy => new HocKyDTO
            {
                Id = hocKy.Id,
                Ten = hocKy.Ten,
                NamHoc = hocKy.NamHoc
            }).ToList();
        }

        public async Task<HocKyDTO?> GetHocKyDTO(int id)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return null;
            }
            return hocKy.toDTOFromHocKy();
        }


        public async Task<HocKyDTO?> UpdateHocKyDTO(int id, CreateHocKyDTO newHocKyDTO)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return null;
            }
            hocKy.Ten = newHocKyDTO.Ten;
            hocKy.NamHoc = newHocKyDTO.NamHoc;
            await _context.SaveChangesAsync();
            return hocKy.toDTOFromHocKy();
        }

      

        public async Task<bool> UpdateHanSuaDiem(int id, DateTime hanSuaDiem)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHanSuaCongThucDiem(int id, DateTime hanSuaCongThucDiem)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}