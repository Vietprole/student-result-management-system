using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class ChucVu
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string TenChucVu { get; set; } = string.Empty;
    }
}