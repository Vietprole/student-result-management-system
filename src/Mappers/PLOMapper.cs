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
            NganhId = pLOModel.NganhId,
            TenNganh = pLOModel.Nganh.Ten,
        };
    }

    public static PLO ToPLOFromCreateDTO(this CreatePLODTO createPLODTO)
    {
        return new PLO
        {
            Ten = createPLODTO.Ten,
            MoTa = createPLODTO.MoTa,
            NganhId = createPLODTO.NganhId,
        };
    }

    public static PLO ToPLOFromUpdateDTO(this UpdatePLODTO updatePLODTO, PLO pLOModel)
    {
        pLOModel.Ten = updatePLODTO.Ten ?? pLOModel.Ten;
        pLOModel.MoTa = updatePLODTO.MoTa ?? pLOModel.MoTa;
        pLOModel.NganhId = updatePLODTO.NganhId ?? pLOModel.NganhId;

        return pLOModel;
    }
}
