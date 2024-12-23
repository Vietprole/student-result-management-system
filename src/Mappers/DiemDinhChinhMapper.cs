using System;
using Student_Result_Management_System.DTOs.DiemDinhChinh;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class DiemDinhChinhMapper
{
    public static DiemDinhChinhDTO ToDiemDinhChinhDTO(this DiemDinhChinh ddc)
    {
        return new DiemDinhChinhDTO
        {
            Id = ddc.Id,
            SinhVienId = ddc.SinhVienId,
            CauHoiId = ddc.CauHoiId,
            DiemMoi = ddc.DiemMoi,
            ThoiDiemMo = DateTime.SpecifyKind(ddc.ThoiDiemMo, DateTimeKind.Utc),
            ThoiDiemDuyet = ddc.ThoiDiemDuyet.HasValue ? DateTime.SpecifyKind(ddc.ThoiDiemDuyet.Value, DateTimeKind.Utc) : null,
            DuocDuyet = ddc.DuocDuyet,
            NguoiDuyetId = ddc.NguoiDuyetId,
            TenNguoiDuyet = ddc.NguoiDuyet?.Ten
        };
    }

    public static DiemDinhChinh ToDiemDinhChinhFromCreateDTO(this CreateDiemDinhChinhDTO createDTO)
    {
        return new DiemDinhChinh
        {
            SinhVienId = createDTO.SinhVienId,
            CauHoiId = createDTO.CauHoiId,
            DiemMoi = createDTO.DiemMoi,
            ThoiDiemMo = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            DuocDuyet = false
        };
    }

    public static DiemDinhChinh ToDiemDinhChinhFromUpdateDTO(this UpdateDiemDinhChinhDTO updateDTO, DiemDinhChinh ddc)
    {
        ddc.SinhVienId = updateDTO.SinhVienId;
        ddc.CauHoiId = updateDTO.CauHoiId;
        ddc.DiemMoi = updateDTO.DiemMoi ?? ddc.DiemMoi;
        ddc.ThoiDiemMo = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        ddc.DuocDuyet = updateDTO.DuocDuyet ?? ddc.DuocDuyet;
        return ddc;
    }
}
