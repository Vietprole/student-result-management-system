using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
