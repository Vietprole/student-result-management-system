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
            KhoaId = sinhVienModel.KhoaId??0, //Để đây hồi tin sửa
            NamBatDau = sinhVienModel.NamBatDau
        };
    }

    public static SinhVien ToSinhVienFromCreateDTO(this CreateSinhVienDTO createSinhVienDTO)
    {
        return new SinhVien
        {
            Ten = createSinhVienDTO.Ten,
        };
    }
}
