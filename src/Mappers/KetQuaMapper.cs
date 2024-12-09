using System;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers;

public static class KetQuaMapper
{
    public static KetQuaDTO ToKetQuaDTO(this KetQua ketQua)
    {
        return new KetQuaDTO
        {
            Id = ketQua.Id,
            //Diem = ketQua.Diem,
            SinhVienId = ketQua.SinhVienId,
            CauHoiId = ketQua.CauHoiId
        };
    }

    public static KetQua ToKetQuaFromCreateDTO(this CreateKetQuaDTO createKetQuaDTO)
    {
        return new KetQua
        {
            //Diem = createKetQuaDTO.Diem,
            SinhVienId = createKetQuaDTO.SinhVienId,
            CauHoiId = createKetQuaDTO.CauHoiId
        };
    }

    public static KetQua ToKetQuaFromUpdate(this UpdateKetQuaDTO updateKetQuaDTO)
    {
        return new KetQua
        {
            //Diem = updateKetQuaDTO.Diem,
            SinhVienId = updateKetQuaDTO.SinhVienId,
            CauHoiId = updateKetQuaDTO.CauHoiId
        };
    }
}
