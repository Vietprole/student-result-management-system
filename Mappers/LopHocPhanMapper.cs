using System;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class LopHocPhanMapper
{
    public static LopHocPhanDTO ToLopHocPhanDTO(this LopHocPhan lopHocPhanModel)
    {
        return new LopHocPhanDTO
        {
            Id = lopHocPhanModel.Id,
            Ten = lopHocPhanModel.Ten,
            HocKy = lopHocPhanModel.HocKy?.MoTa ?? string.Empty,
            HocPhan = lopHocPhanModel.HocPhan.Ten,
        };
    }
}
