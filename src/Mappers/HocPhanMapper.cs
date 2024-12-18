using System;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class HocPhanMapper
{
    public static HocPhanDTO ToHocPhanDTO(this HocPhan hocPhan)
    {
        return new HocPhanDTO
        {
            Id = hocPhan.Id,
            Ten = hocPhan.Ten,
            SoTinChi = hocPhan.SoTinChi,
            LaCotLoi = hocPhan.LaCotLoi,
            KhoaId = hocPhan.KhoaId,
            MaHocPhan = hocPhan.MaHocPhan,
            TenKhoa = hocPhan.Khoa.Ten,
        };
    }

    public static HocPhan ToHocPhanFromCreateDTO(this CreateHocPhanDTO createHocPhanDTO)
    {
        return new HocPhan
        {
            Ten = createHocPhanDTO.Ten,
            SoTinChi = createHocPhanDTO.SoTinChi,
            LaCotLoi = createHocPhanDTO.LaCotLoi,
            KhoaId = createHocPhanDTO.KhoaId,
        };
    }

    public static HocPhan ToHocPhanFromUpdateDTO(this UpdateHocPhanDTO updateHocPhanDTO, HocPhan hocPhan)
    {
        hocPhan.Ten = updateHocPhanDTO.Ten ?? hocPhan.Ten;
        hocPhan.SoTinChi = updateHocPhanDTO.SoTinChi ?? hocPhan.SoTinChi;
        hocPhan.LaCotLoi = updateHocPhanDTO.LaCotLoi ?? hocPhan.LaCotLoi;
        hocPhan.KhoaId = updateHocPhanDTO.KhoaId ?? hocPhan.KhoaId;

        return hocPhan;
    }
}
