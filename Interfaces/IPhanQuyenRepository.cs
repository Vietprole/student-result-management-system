using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IPhanQuyenRepository
    {
        Task<List<PhanQuyen>> GetAllPhanQuyen();
        Task<PhanQuyen?> GetPhanQuyenById(int id);
        Task<PhanQuyen> CreatePhanQuyen(PhanQuyen phanQuyen);
        Task<PhanQuyen?> UpdatePhanQuyen(int id,PhanQuyen phanQuyen);
        Task<PhanQuyen?> DeletePhanQuyen(int id);
    }
}