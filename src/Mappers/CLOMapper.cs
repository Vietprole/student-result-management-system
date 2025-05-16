using System;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class CLOMapper
{
    public static CLODTO ToCLODTO(this CLO clo)
    {
        return new CLODTO
        {
            Id = clo.Id,
            Ten = clo.Ten,
            MoTa = clo.MoTa,
            HocPhanId = clo.HocPhanId,
            TenHocPhan = clo.HocPhan.Ten,
            HocKyId = clo.HocKyId ?? 0,
            TenHocKy = clo.HocKy != null ? $"{clo.HocKy.Ten} - {clo.HocKy.NamHoc}" : string.Empty,
        };
    }

    public static CLO ToCLOFromCreateDTO(this CreateCLODTO createCLODTO)
    {
        return new CLO
        {
            Ten = createCLODTO.Ten,
            MoTa = createCLODTO.MoTa,
            HocPhanId = createCLODTO.HocPhanId,
            HocKyId = createCLODTO.HocKyId,
        };
    }

    public static CLO ToCLOFromUpdateDTO(this UpdateCLODTO updateCLODTO, CLO existingCLO)
    {
        existingCLO.Ten = updateCLODTO.Ten ?? existingCLO.Ten;
        existingCLO.MoTa = updateCLODTO.MoTa ?? existingCLO.MoTa;
        existingCLO.HocPhanId = updateCLODTO.HocPhanId ?? existingCLO.HocPhanId;
        existingCLO.HocKyId = updateCLODTO.HocKyId ?? existingCLO.HocKyId;
        
        return existingCLO;
    }
}
