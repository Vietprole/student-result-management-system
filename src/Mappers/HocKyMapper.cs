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
        public static HocKyDTO ToHocKyDTO(this HocKy hocKy)
        {
            return new HocKyDTO
            {
                Id = hocKy.Id,
                Ten = hocKy.Ten,
                NamHoc = hocKy.NamHoc+ "-" + (hocKy.NamHoc+1).ToString(),
                MaHocKy = hocKy.MaHocKy,
                TenHienThi = $"{hocKy.Ten} - {hocKy.NamHoc}"
            };
        }

        public static HocKy ToHocKyFromCreateDTO(this CreateHocKyDTO createHocKyDTO)
        {
            return new HocKy
            {
                Ten = createHocKyDTO.Ten,
                NamHoc = createHocKyDTO.NamHoc
            };
        }

        public static HocKy ToHocKyFromUpdateDTO(this UpdateHocKyDTO updateHocKyDTO, HocKy existingHocKy)
        {
            existingHocKy.Ten = updateHocKyDTO.Ten ?? existingHocKy.Ten;
            existingHocKy.NamHoc = updateHocKyDTO.NamHoc ?? existingHocKy.NamHoc;
            existingHocKy.MaHocKy = updateHocKyDTO.MaHocKy ?? existingHocKy.MaHocKy;

            return existingHocKy;
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