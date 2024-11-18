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
            NganhId = hocPhan.NganhId,
        };
    }

    public static HocPhan ToHocPhanFromCreateDTO(this CreateHocPhanDTO createHocPhanDTO)
    {
        return new HocPhan
        {
            Ten = createHocPhanDTO.Ten,
            SoTinChi = createHocPhanDTO.SoTinChi,
            LaCotLoi = createHocPhanDTO.LaCotLoi,
            NganhId = createHocPhanDTO.NganhId,
        };
    }

    public static HocPhan ToHocPhanFromUpdateDTO(this UpdateHocPhanDTO updateHocPhanDTO)
    {
        return new HocPhan
        {
            Ten = updateHocPhanDTO.Ten,
            SoTinChi = updateHocPhanDTO.SoTinChi,
            LaCotLoi = updateHocPhanDTO.LaCotLoi,
            NganhId = updateHocPhanDTO.NganhId,
        };
    }
}
