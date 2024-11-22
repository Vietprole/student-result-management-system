using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class ChucVuRepository : IChucVuRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public ChucVuRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<string?> GetIdChucVu(string chucvu)
        {
            var role = await _roleManager.FindByNameAsync(chucvu);
            if (role != null)
            {
                return role.Id;
            }
            else
            {
                return null;
            }
        }
    }
}