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
        public async Task<HocKyDTO?> AddHocKyDTO(CreateHocKyDTO newHocKyDTO)
        {

            string MaHocKi = String.Empty;
            if(newHocKyDTO.Ten.ToString()=="Kỳ 1")
            {
                MaHocKi = "10";
            }else if(newHocKyDTO.Ten.ToString()=="Kỳ 2")
            {
                MaHocKi = "20";
            }else if(newHocKyDTO.Ten.ToString()=="Kỳ hè")
            {
                MaHocKi = "21";
            }
            else
            {
                return null;
            }

            string result = newHocKyDTO.NamHoc.ToString().Substring(newHocKyDTO.NamHoc.ToString().Length - 2);
            var exits = await _context.HocKies.FirstOrDefaultAsync(x=>x.MaHocKy==result+MaHocKi);
            if(exits!=null)
            {
                return null;
            }
            var hocKy = newHocKyDTO.toHocKyFromNewDTO();
            hocKy.MaHocKy=result+MaHocKi;
            await _context.HocKies.AddAsync(hocKy);
            await _context.SaveChangesAsync();
            return hocKy.ToHocKyDTO();

        }
        public bool CheckTenHocKy(string tenHocKy)
        {
            if (string.IsNullOrEmpty(tenHocKy))
            {
                return false;   
            }
            // Kiểm tra chuỗi có bằng đúng một trong các cụm từ yêu cầu
            return tenHocKy == "Kỳ 1" || tenHocKy == "Kỳ 2" || tenHocKy == "Kỳ hè";
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
            return hocKies.Select(hocKy => hocKy.ToHocKyDTO()).ToList();
        }

        public async Task<HocKyDTO?> GetHocKyDTO(int id)
        {
            var hocKy = await _context.HocKies.FindAsync(id);
            if (hocKy == null)
            {
                return null;
            }
            return hocKy.ToHocKyDTO();
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
            return hocKy.ToHocKyDTO();
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