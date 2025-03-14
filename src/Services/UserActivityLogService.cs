using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Data;

namespace Student_Result_Management_System.Services
{
    public class UserActivityLogService(ApplicationDBContext context) : IUserActivityLogService
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<bool> LogUserActivity(UserActivityLog userActivityLog)
        {
            await _context.UserActivityLogs.AddAsync(userActivityLog);
            // SaveChanges returns the number of rows affected
            return await _context.SaveChangesAsync() > 0;
        }
    }
}