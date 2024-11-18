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

    public static CLO ToCLOFromUpdateDTO(this UpdateCLODTO updateCLODTO)
    {
        return new CLO
        {
            Ten = updateCLODTO.Ten,
            MoTa = updateCLODTO.MoTa,
            LopHocPhanId = updateCLODTO.LopHocPhanId,
        };
    }
}
