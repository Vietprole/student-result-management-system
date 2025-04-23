using Student_Result_Management_System.Data;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Interfaces;

namespace Student_Result_Management_System.Services
{
    public class UserActivityLogService(ApplicationDBContext dbContext) : IUserActivityLogService
    {
        private readonly ApplicationDBContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task LogUserActivityAsync(UserActivityLog log)
        {
            ArgumentNullException.ThrowIfNull(log);

            await _dbContext.UserActivityLogs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}
