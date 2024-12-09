using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Student_Result_Management_System.Models
{
    public class TaiKhoan : IdentityUser
    {
        public string Ten { get; set; } = string.Empty;
    }
}