using System;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class NganhMapper
{
    public static NganhDTO ToNganhDTO(this Nganh nganh)
    {
        return new NganhDTO
        {
            Id = nganh.Id,
            Ten = nganh.Ten,
            KhoaId = nganh.KhoaId,
            TenKhoa = nganh.Khoa?.Ten ?? string.Empty,
        };
    }

    public static Nganh ToNganhFromCreateDTO(this CreateNganhDTO createNganhDTO)
    {
        return new Nganh
        {
            Ten = createNganhDTO.Ten,
            KhoaId = createNganhDTO.KhoaId,
        };
    }

    public static Nganh ToNganhFromUpdateDTO(this UpdateNganhDTO updateNganhDTO)
    {
        return new Nganh
        {
            Ten = updateNganhDTO.Ten,
            KhoaId = updateNganhDTO.KhoaId,
        };
    }
}
