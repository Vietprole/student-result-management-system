using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class SinhVienRepository:ISinhVienRepository
    {
        private readonly ApplicationDBContext _context;
        public SinhVienRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<SinhVien?> getSinhVienByTaiKhoanId(int id)
        {
            return await _context.SinhViens.FirstOrDefaultAsync(x => x.TaiKhoanId == id);
        }
    }
}