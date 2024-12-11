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
            LopHocPhanId = clo.LopHocPhanId,
        };
    }

    public static CLO ToCLOFromCreateDTO(this CreateCLODTO createCLODTO)
    {
        return new CLO
        {
            Ten = createCLODTO.Ten,
            MoTa = createCLODTO.MoTa,
            LopHocPhanId = createCLODTO.LopHocPhanId,
        };
    }

    public static CLO ToCLOFromUpdateDTO(this UpdateCLODTO updateCLODTO, CLO existingCLO)
    {
        existingCLO.Ten = updateCLODTO.Ten ?? existingCLO.Ten;
        existingCLO.MoTa = updateCLODTO.MoTa ?? existingCLO.MoTa;
        existingCLO.LopHocPhanId = updateCLODTO.LopHocPhanId ?? existingCLO.LopHocPhanId;
        
        return existingCLO;
    }
}
