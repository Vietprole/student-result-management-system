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
        };
    }

    public static CauHoi ToCauHoiFromCreateDTO(this CreateCauHoiDTO createCauHoiDTO)
    {
        return new CauHoi
        {
            Ten = createCauHoiDTO.Ten,
            TrongSo = createCauHoiDTO.TrongSo,
            BaiKiemTraId = createCauHoiDTO.BaiKiemTraId,
        };
    }
}
