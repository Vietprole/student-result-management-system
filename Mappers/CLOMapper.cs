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
            Mota = clo.Mota,
            LopHocPhanId = clo.LopHocPhanId,
        };
    }

    public static CLO ToCLOFromCreateDTO(this CreateCLODTO createCLODTO)
    {
        return new CLO
        {
            Ten = createCLODTO.Ten,
            Mota = createCLODTO.Mota,
            LopHocPhanId = createCLODTO.LopHocPhanId,
        };
    }

    public static CLO ToCLOFromUpdateDTO(this UpdateCLODTO updateCLODTO)
    {
        return new CLO
        {
            Ten = updateCLODTO.Ten,
            Mota = updateCLODTO.Mota,
            LopHocPhanId = updateCLODTO.LopHocPhanId,
        };
    }
}
