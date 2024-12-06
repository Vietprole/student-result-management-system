using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.KiHoc
{
    public class ViewKiHocDTO
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string NamHoc { get; set; } = string.Empty;
        public DateTime? HanSuaDiem { get; set; }
        public DateTime? HanSuaCongThucDiem { get; set; }
    }
}