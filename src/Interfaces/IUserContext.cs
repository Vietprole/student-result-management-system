
using System.Net;

namespace Student_Result_Management_System.Interfaces
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        int UserId { get; }
        string UserName { get; }
        string UserRole { get; }
        IPAddress UserIpAddress { get; }
    }
}