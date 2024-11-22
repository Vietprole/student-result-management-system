using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.ChucVu;

namespace Student_Result_Management_System.Mappers
{
    public static class ChucVuMapper
    {
        public static ChucVuDTO ToChucVuDTOFromString(this string chucVu)
        {
            return new ChucVuDTO
            {
                TenChucVu = chucVu
            };
        }
    }
}