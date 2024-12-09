using System;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class LopHocPhanMapper
{
    public static LopHocPhanDTO ToLopHocPhanDTO(this LopHocPhan lopHocPhanModel)
    {
        return new LopHocPhanDTO
        {
            Id = lopHocPhanModel.Id,
            Ten = lopHocPhanModel.Ten,
            HocPhanId = lopHocPhanModel.HocPhanId,
            KiHocId = lopHocPhanModel.HocKyId
        };
    }

    public static LopHocPhan ToLopHocPhanFromCreateDTO(this CreateLopHocPhanDTO createLopHocPhanDTO)
    {
        return new LopHocPhan
        {
            Ten = createLopHocPhanDTO.Ten,
            HocPhanId = createLopHocPhanDTO.HocPhanId,
            HocKyId = createLopHocPhanDTO.KiHocId
        };
    }

    public static LopHocPhan ToLopHocPhanFromUpdateDTO(this UpdateLopHocPhanDTO updateLopHocPhanDTO)
    {
        return new LopHocPhan
        {
            Ten = updateLopHocPhanDTO.Ten,
            HocPhanId = updateLopHocPhanDTO.HocPhanId,
            HocKyId = updateLopHocPhanDTO.KiHocId
        };
    }
}
