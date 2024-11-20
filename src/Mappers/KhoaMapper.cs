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
        };
    }

    public static Khoa ToKhoaFromCreateDTO(this CreateKhoaDTO createKhoaDTO)
    {
        return new Khoa
        {
            Ten = createKhoaDTO.Ten,
        };
    }

    public static Khoa ToKhoaFromUpdateDTO(this UpdateKhoaDTO updateKhoaDTO)
    {
        return new Khoa
        {
            Ten = updateKhoaDTO.Ten,
        };
    }
}
