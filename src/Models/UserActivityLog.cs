namespace Student_Result_Management_System.Models;

public class UserActivityLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string? EntityBefore { get; set; }
    public string? EntityAfter { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
