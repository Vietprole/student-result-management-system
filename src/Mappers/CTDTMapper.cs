using System;
using Student_Result_Management_System.DTOs.CTDT;
using Student_Result_Management_System.Models;
namespace Student_Result_Management_System.Mappers;

public static class CTDTMapper
{
    public static CTDTDTO ToCTDTDTO(this CTDT ctdt)
    {
        return new CTDTDTO
        {
            Id = ctdt.Id,
            Ten = ctdt.Ten,
            NganhId = ctdt.NganhId,
        };
    }

    public static CTDT ToCTDTFromCreateDTO(this CreateCTDTDTO createCTDTDTO)
    {
        return new CTDT
        {
            Ten = createCTDTDTO.Ten,
            NganhId = createCTDTDTO.NganhId,
        };
    }

    public static CTDT ToCTDTFromUpdateDTO(this UpdateCTDTDTO updateCTDTDTO)
    {
        return new CTDT
        {
            Ten = updateCTDTDTO.Ten,
            NganhId = updateCTDTDTO.NganhId,
        };
    }
}
