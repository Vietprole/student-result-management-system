using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<string>> GetListChucVu()
        {
            List<string> list_roles = new List<string>();
            var roles = await _roleManager.Roles.Select(x => x.Name).ToListAsync(); // Asynchronous call to fetch roles
            if(roles.Count == 0) // If no roles are found
            {
                return list_roles; // Return empty list
            }
            list_roles.AddRange(roles.Where(role => role != null)!); // Add roles to the list, excluding null values
            return list_roles; // Return the list of roles
        }

    }
}