using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Student_Result_Management_System.DTOs.HocKy;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers
{
    public static class HocKyMapper
    {
        public static HocKy toHocKyFromDTO(this HocKyDTO viewHocKyDTO)
        {
            return new HocKy
            {
                Id = viewHocKyDTO.Id,
                Ten = viewHocKyDTO.Ten,
                NamHoc = viewHocKyDTO.NamHoc,
                HanSuaDiem = viewHocKyDTO.HanSuaDiem,
                HanSuaCongThucDiem = viewHocKyDTO.HanSuaCongThucDiem
            };
        }
        public static HocKyDTO toDTOFromHocKy(this HocKy hocKy)
        {
            return new HocKyDTO
            {
                Id = hocKy.Id,
                Ten = hocKy.Ten,
                NamHoc = hocKy.NamHoc,
                HanSuaDiem = hocKy.HanSuaDiem,
                HanSuaCongThucDiem = hocKy.HanSuaCongThucDiem

            };
        }
        public static HocKy toHocKyFromNewDTO(this CreateHocKyDTO newHocKyDTO)
        {
            return new HocKy
            {
                Ten = newHocKyDTO.Ten,
                NamHoc = newHocKyDTO.NamHoc
            };
        }
        
    }
}