using System;
using Student_Result_Management_System.DTOs.Khoa;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class KhoaMapper
{
    public static KhoaDTO ToKhoaDTO(this Khoa khoa)
    {
        return new KhoaDTO
        {
            Id = khoa.Id,
            Ten = khoa.Ten,
            MaKhoa = khoa.MaKhoa,
        };
    }

    public static Khoa ToKhoaFromCreateDTO(this CreateKhoaDTO createKhoaDTO)
    {
        return new Khoa
        {
            Ten = createKhoaDTO.Ten,
            MaKhoa = createKhoaDTO.MaKhoa,
        };
    }

    public static Khoa ToKhoaFromUpdateDTO(this UpdateKhoaDTO updateKhoaDTO, Khoa existingKhoa)
    {
        existingKhoa.Ten = updateKhoaDTO.Ten ?? existingKhoa.Ten;
        existingKhoa.MaKhoa = updateKhoaDTO.MaKhoa ?? existingKhoa.MaKhoa;
        return existingKhoa;
    }
}
