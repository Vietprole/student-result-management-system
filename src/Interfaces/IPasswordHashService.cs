using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Interfaces
{
    public interface IPasswordHashService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPasswordWithSalt);
    }
}