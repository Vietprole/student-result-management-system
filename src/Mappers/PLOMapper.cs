using System;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class PLOMapper
{
    public static PLODTO ToPLODTO(this PLO pLOModel)
    {
        return new PLODTO
        {
            Id = pLOModel.Id,
            Ten = pLOModel.Ten,
            MoTa = pLOModel.MoTa,
            CTDTId = pLOModel.CTDTId,
            TenCTDT = pLOModel.CTDT?.Ten ?? string.Empty,
        };
    }

    public static PLO ToPLOFromCreateDTO(this CreatePLODTO createPLODTO)
    {
        return new PLO
        {
            Ten = createPLODTO.Ten,
            MoTa = createPLODTO.MoTa,
            CTDTId = createPLODTO.CTDTId,
        };
    }
}
