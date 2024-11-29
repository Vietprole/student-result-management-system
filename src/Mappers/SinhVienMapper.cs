using System;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class SinhVienMapper
{
    public static SinhVienDTO ToSinhVienDTO(this SinhVien sinhVienModel)
    {
        return new SinhVienDTO
        {
            Id = sinhVienModel.Id,
            Ten = sinhVienModel.Ten,
        };
    }

    public static SinhVien ToSinhVienFromCreateDTO(this CreateSinhVienDTO createSinhVienDTO)
    {
        return new SinhVien
        {
            Ten = createSinhVienDTO.Ten,
        };
    }
    public static SinhVienLoginDTO ToSinhVienLoginDTO(this SinhVien sinhVienModel)
    {
        return new SinhVienLoginDTO
        {
            Id = sinhVienModel.Id,
            Ten = sinhVienModel.Ten,
            TaiKhoanId = sinhVienModel.TaiKhoanId
        };
    }
}
