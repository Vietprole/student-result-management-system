using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IUserActivityLogService
    {
        Task LogUserActivityAsync(UserActivityLog log);
    }
}
