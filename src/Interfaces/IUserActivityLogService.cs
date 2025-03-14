using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IUserActivityLogService
    {
        public Task<bool> LogUserActivity(UserActivityLog userActivityLog);
    }
}