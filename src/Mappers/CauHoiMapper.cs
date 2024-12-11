using System;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static  class CauHoiMapper
{
    public static CauHoiDTO ToCauHoiDTO(this CauHoi cauHoi)
    {
        return new CauHoiDTO
        {
            Id = cauHoi.Id,
            Ten = cauHoi.Ten,
            TrongSo = cauHoi.TrongSo,
            BaiKiemTraId = cauHoi.BaiKiemTraId,
            ThangDiem = cauHoi.ThangDiem
        };
    }

    public static CauHoi ToCauHoiFromCreateDTO(this CreateCauHoiDTO createCauHoiDTO)
    {
        return new CauHoi
        {
            Ten = createCauHoiDTO.Ten,
            TrongSo = createCauHoiDTO.TrongSo,
            BaiKiemTraId = createCauHoiDTO.BaiKiemTraId,
            ThangDiem = createCauHoiDTO.ThangDiem
        };
    }

    public static CauHoi ToCauHoiFromUpdateDTO(this UpdateCauHoiDTO updateDTO, CauHoi existingCauHoi)
    {
        existingCauHoi.Ten = updateDTO.Ten ?? existingCauHoi.Ten;
        existingCauHoi.TrongSo = updateDTO.TrongSo ?? existingCauHoi.TrongSo;
        existingCauHoi.BaiKiemTraId = updateDTO.BaiKiemTraId ?? existingCauHoi.BaiKiemTraId;
        existingCauHoi.ThangDiem = updateDTO.ThangDiem ?? existingCauHoi.ThangDiem;

        return existingCauHoi;
    }
}
