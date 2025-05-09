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
            MaNganh = nganh.MaNganh,
            KhoaId = nganh.KhoaId,
            TenKhoa = nganh.Khoa?.Ten ?? string.Empty,
            NguoiQuanLyId = nganh.TaiKhoanId,
            TenNguoiQuanLy = nganh.TaiKhoan?.Ten ?? string.Empty,
        };
    }

    public static Nganh ToNganhFromCreateDTO(this CreateNganhDTO createNganhDTO)
    {
        return new Nganh
        {
            Ten = createNganhDTO.Ten,
            KhoaId = createNganhDTO.KhoaId,
            TaiKhoanId = createNganhDTO.NguoiQuanLyId,
        };
    }

    public static Nganh ToNganhFromUpdateDTO(this UpdateNganhDTO updateNganhDTO, Nganh existingNganh)
    {
        existingNganh.Ten = updateNganhDTO.Ten ?? existingNganh.Ten;
        existingNganh.TaiKhoanId = updateNganhDTO.NguoiQuanLyId ?? existingNganh.TaiKhoanId;
        return existingNganh;
    }
}
