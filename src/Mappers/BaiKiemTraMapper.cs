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
            TrongSoDeXuat = baiKiemTra.TrongSoDeXuat,
            // NgayMoNhapDiem = baiKiemTra.NgayMoNhapDiem.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(baiKiemTra.NgayMoNhapDiem.Value, TimeZoneInfo.Local) : (DateTime?)null,
            // HanNhapDiem = baiKiemTra.HanNhapDiem.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(baiKiemTra.HanNhapDiem.Value, TimeZoneInfo.Local) : (DateTime?)null,
            // HanDinhChinh = baiKiemTra.HanDinhChinh.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(baiKiemTra.HanDinhChinh.Value, TimeZoneInfo.Local) : (DateTime?)null,
            // NgayXacNhan = baiKiemTra.NgayXacNhan.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(baiKiemTra.NgayXacNhan.Value, TimeZoneInfo.Local) : (DateTime?)null,
            // NgayMoNhapDiem = baiKiemTra.NgayMoNhapDiem,
            // HanNhapDiem = baiKiemTra.HanNhapDiem,
            // HanDinhChinh = baiKiemTra.HanDinhChinh,
            // NgayXacNhan = baiKiemTra.NgayXacNhan,
            NgayMoNhapDiem = baiKiemTra.NgayMoNhapDiem.HasValue ?
            DateTime.SpecifyKind(baiKiemTra.NgayMoNhapDiem.Value, DateTimeKind.Utc) : null,
            HanNhapDiem = baiKiemTra.HanNhapDiem.HasValue ?
            DateTime.SpecifyKind(baiKiemTra.HanNhapDiem.Value, DateTimeKind.Utc) : null,
            HanDinhChinh = baiKiemTra.HanDinhChinh.HasValue ?
            DateTime.SpecifyKind(baiKiemTra.HanDinhChinh.Value, DateTimeKind.Utc) : null,
            NgayXacNhan = baiKiemTra.NgayXacNhan.HasValue ?
            DateTime.SpecifyKind(baiKiemTra.NgayXacNhan.Value, DateTimeKind.Utc) : null,
            LopHocPhanId = baiKiemTra.LopHocPhanId,
        };
    }

    public static BaiKiemTra ToBaiKiemTraFromCreateDTO(this CreateBaiKiemTraDTO createBaiKiemTraDTO)
    {
        return new BaiKiemTra
        {
            Loai = createBaiKiemTraDTO.Loai,
            TrongSo = createBaiKiemTraDTO.TrongSo,
            TrongSoDeXuat = createBaiKiemTraDTO.TrongSoDeXuat,
            NgayMoNhapDiem = createBaiKiemTraDTO.NgayMoNhapDiem,
            HanNhapDiem = createBaiKiemTraDTO.HanNhapDiem,
            HanDinhChinh = createBaiKiemTraDTO.HanDinhChinh,
            LopHocPhanId = createBaiKiemTraDTO.LopHocPhanId,
        };
    }

    public static BaiKiemTra ToBaiKiemTraFromUpdateDTO(this UpdateBaiKiemTraDTO updateDTO, BaiKiemTra existingBaiKiemTra)
    {
        existingBaiKiemTra.Loai = updateDTO.Loai ?? existingBaiKiemTra.Loai;
        existingBaiKiemTra.TrongSo = updateDTO.TrongSo ?? existingBaiKiemTra.TrongSo;
        existingBaiKiemTra.TrongSoDeXuat = updateDTO.TrongSoDeXuat ?? existingBaiKiemTra.TrongSoDeXuat;
        existingBaiKiemTra.NgayMoNhapDiem = updateDTO.NgayMoNhapDiem ?? existingBaiKiemTra.NgayMoNhapDiem;
        existingBaiKiemTra.HanNhapDiem = updateDTO.HanNhapDiem ?? existingBaiKiemTra.HanNhapDiem;
        existingBaiKiemTra.HanDinhChinh = updateDTO.HanDinhChinh ?? existingBaiKiemTra.HanDinhChinh;
        existingBaiKiemTra.NgayXacNhan = updateDTO.NgayXacNhan ?? existingBaiKiemTra.NgayXacNhan;

        return existingBaiKiemTra;
    }
}
