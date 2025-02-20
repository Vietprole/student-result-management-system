using System;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class LopHocPhanMapper
{
    private static readonly TimeZoneInfo VietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

    public static LopHocPhanDTO ToLopHocPhanDTO(this LopHocPhan lopHocPhanModel)
    {
        return new LopHocPhanDTO
        {
            Id = lopHocPhanModel.Id,
            MaLopHocPhan = lopHocPhanModel.MaLopHocPhan,
            Ten = lopHocPhanModel.Ten,
            HocPhanId = lopHocPhanModel.HocPhanId,
            HocKyId = lopHocPhanModel.HocKyId,
            TenHocPhan = lopHocPhanModel.HocPhan.Ten,
            TenHocKy = lopHocPhanModel.HocKy.Ten,
            // HanDeXuatCongThucDiem = TimeZoneInfo.ConvertTimeFromUtc(lopHocPhanModel.HanDeXuatCongThucDiem, TimeZoneInfo.Local),
            HanDeXuatCongThucDiem = DateTime.SpecifyKind(lopHocPhanModel.HanDeXuatCongThucDiem, DateTimeKind.Utc),
            GiangVienId = lopHocPhanModel.GiangVienId ?? 0,
            TenGiangVien = lopHocPhanModel.GiangVien?.TaiKhoan?.Ten ?? string.Empty,
            NamHoc = lopHocPhanModel.HocKy.NamHoc+" - "+(lopHocPhanModel.HocKy.NamHoc+1)
        };
    }

    public static LopHocPhan ToLopHocPhanFromCreateDTO(this CreateLopHocPhanDTO createLopHocPhanDTO)
    {
        return new LopHocPhan
        {
            Ten = createLopHocPhanDTO.Ten,
            HocPhanId = createLopHocPhanDTO.HocPhanId,
            HocKyId = createLopHocPhanDTO.HocKyId,
            HanDeXuatCongThucDiem = createLopHocPhanDTO.HanDeXuatCongThucDiem,
            GiangVienId = createLopHocPhanDTO.GiangVienId,
        };
    }

    public static LopHocPhan ToLopHocPhanFromUpdateDTO(this UpdateLopHocPhanDTO updateLopHocPhanDTO, LopHocPhan existingLopHocPhan)
    {
        existingLopHocPhan.Ten = updateLopHocPhanDTO.Ten ?? existingLopHocPhan.Ten;
        existingLopHocPhan.HocPhanId = updateLopHocPhanDTO.HocPhanId ?? existingLopHocPhan.HocPhanId;
        existingLopHocPhan.HocKyId = updateLopHocPhanDTO.HocKyId ?? existingLopHocPhan.HocKyId;
        existingLopHocPhan.HanDeXuatCongThucDiem = updateLopHocPhanDTO.HanDeXuatCongThucDiem ?? existingLopHocPhan.HanDeXuatCongThucDiem;
        existingLopHocPhan.GiangVienId = updateLopHocPhanDTO.GiangVienId ?? existingLopHocPhan.GiangVienId;
        
        return existingLopHocPhan;
    }
}
