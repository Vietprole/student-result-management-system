using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Result_Management_System.DTOs.KiHoc;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers
{
    public static class KiHocMapper
    {
        public static KiHoc toKiHocFromDTO(this ViewKiHocDTO viewKiHocDTO)
        {
            return new KiHoc
            {
                Id = viewKiHocDTO.Id,
                Ten = viewKiHocDTO.Ten,
                NamHoc = viewKiHocDTO.NamHoc,
                HanSuaDiem = viewKiHocDTO.HanSuaDiem,
                HanSuaCongThucDiem = viewKiHocDTO.HanSuaCongThucDiem
            };
        }
        public static ViewKiHocDTO toDTOFromKiHoc(this KiHoc kiHoc)
        {
            return new ViewKiHocDTO
            {
                Id = kiHoc.Id,
                Ten = kiHoc.Ten,
                NamHoc = kiHoc.NamHoc,
                HanSuaDiem = kiHoc.HanSuaDiem,
                HanSuaCongThucDiem = kiHoc.HanSuaCongThucDiem

            };
        }
        public static KiHoc toKiHocFromNewDTO(this NewKiHocDTO newKiHocDTO)
        {
            return new KiHoc
            {
                Ten = newKiHocDTO.Ten,
                NamHoc = newKiHocDTO.NamHoc
            };
        }
        
    }
}