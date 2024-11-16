using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ISinhVienRepository
    {
        Task<SinhVien?> getSinhVienByTaiKhoanId(int id);
    }
}