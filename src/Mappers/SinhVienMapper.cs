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
            Ten=sinhVienModel.TaiKhoan.Ten,
            //KhoaId = sinhVienModel.KhoaId??0, //Để đây hồi tin sửa
            TenKhoa = sinhVienModel.Khoa?.Ten ?? string.Empty,
            NamBatDau = sinhVienModel.KhoaNhapHoc
        };
    }

    public static SinhVien ToSinhVienFromCreateDTO(this CreateSinhVienDTO createSinhVienDTO)
    {
        return new SinhVien
        {
            KhoaId = createSinhVienDTO.KhoaId,
            KhoaNhapHoc = createSinhVienDTO.NamBatDau
        };
    }
}
