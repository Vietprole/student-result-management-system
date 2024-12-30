using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(TaiKhoan taikhoan);
        public Task<string> GetFullNameAndRole(string token);
    }
}