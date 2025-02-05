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
            DiemTam = ketQua.DiemTam,
            DiemChinhThuc = ketQua.DiemChinhThuc,
            DaCongBo = ketQua.DaCongBo,
            DaXacNhan = ketQua.DaXacNhan,
            SinhVienId = ketQua.SinhVienId,
            CauHoiId = ketQua.CauHoiId
        };
    }

    public static KetQua ToKetQuaFromCreateDTO(this CreateKetQuaDTO createKetQuaDTO)
    {
        return new KetQua
        {
            DiemTam = createKetQuaDTO.DiemTam,
            DiemChinhThuc = createKetQuaDTO.DiemChinhThuc,
            DaCongBo = createKetQuaDTO.DaCongBo,
            DaXacNhan = createKetQuaDTO.DaXacNhan,
            SinhVienId = createKetQuaDTO.SinhVienId,
            CauHoiId = createKetQuaDTO.CauHoiId
        };
    }

    public static KetQua ToKetQuaFromUpdateDTO(this UpdateKetQuaDTO updateKetQuaDTO, KetQua existingKetQua)
    {
        existingKetQua.SinhVienId = updateKetQuaDTO.SinhVienId;
        existingKetQua.CauHoiId = updateKetQuaDTO.CauHoiId;
        existingKetQua.DiemTam = updateKetQuaDTO.DiemTam ?? existingKetQua.DiemTam;
        existingKetQua.DiemChinhThuc = updateKetQuaDTO.DiemChinhThuc ?? existingKetQua.DiemChinhThuc;
        existingKetQua.DaCongBo = updateKetQuaDTO.DaCongBo ?? existingKetQua.DaCongBo;
        existingKetQua.DaXacNhan = updateKetQuaDTO.DaXacNhan ?? existingKetQua.DaXacNhan;

        return existingKetQua;
    }
}
