using System;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class BaiKiemTraMapper
{
    public static BaiKiemTraDTO ToBaiKiemTraDTO(this BaiKiemTra baiKiemTra)
    {
        return new BaiKiemTraDTO
        {
            Id = baiKiemTra.Id,
            Loai = baiKiemTra.Loai,
            TrongSo = baiKiemTra.TrongSo,
            LopHocPhanId = baiKiemTra.LopHocPhanId,
            CauHois = baiKiemTra.CauHois.Select(cauHoi => cauHoi.ToCauHoiDTO()).ToList(),
        };
    }

    public static BaiKiemTra ToBaiKiemTraFromCreateDTO(this CreateBaiKiemTraDTO createBaiKiemTraDTO)
    {
        return new BaiKiemTra
        {
            Loai = createBaiKiemTraDTO.Loai,
            TrongSo = createBaiKiemTraDTO.TrongSo,
            LopHocPhanId = createBaiKiemTraDTO.LopHocPhanId,
        };
    }
}
