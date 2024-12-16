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
            HocKyId = lopHocPhanModel.HocKyId
        };
    }

    public static LopHocPhan ToLopHocPhanFromCreateDTO(this CreateLopHocPhanDTO createLopHocPhanDTO)
    {
        return new LopHocPhan
        {
            Ten = createLopHocPhanDTO.Ten,
            HocPhanId = createLopHocPhanDTO.HocPhanId,
            HocKyId = createLopHocPhanDTO.HocKyId
        };
    }

    public static LopHocPhan ToLopHocPhanFromUpdateDTO(this UpdateLopHocPhanDTO updateLopHocPhanDTO, LopHocPhan existingLopHocPhan)
    {
        existingLopHocPhan.Ten = updateLopHocPhanDTO.Ten ?? existingLopHocPhan.Ten;
        existingLopHocPhan.HocPhanId = updateLopHocPhanDTO.HocPhanId ?? existingLopHocPhan.HocPhanId;
        existingLopHocPhan.HocKyId = updateLopHocPhanDTO.HocKyId ?? existingLopHocPhan.HocKyId;
        
        return existingLopHocPhan;
    }
}
