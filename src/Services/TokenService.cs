using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly ApplicationDBContext _context;
        public TokenService(IConfiguration config, ApplicationDBContext context)
        {
            _config = config;
            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrEmpty(signingKey))
            {
                throw new ArgumentNullException("JWT:SigningKey", "Signing key cannot be null or empty.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _context = context;

        }
        public async Task<string> CreateToken(TaiKhoan user)
        {
            var giangVienId = await _context.GiangViens.Where(gv => gv.TaiKhoanId == user.Id).Select(gv => gv.Id).FirstOrDefaultAsync();
            var sinhVienId = await _context.SinhViens.Where(sv => sv.TaiKhoanId == user.Id).Select(sv => sv.Id).FirstOrDefaultAsync();
            var nguoiQuanLyCTDTId = await _context.TaiKhoans.Where(tk => tk.Id == user.Id)
                .Select(tk => tk.Id)
                .FirstOrDefaultAsync();
             var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("userId",user.Id.ToString()),
                new Claim("fullname",user.Ten),
                new Claim(ClaimTypes.Role,user.ChucVu.TenChucVu),
                new Claim("giangVienId", giangVienId.ToString() ?? ""),
                new Claim("sinhVienId", sinhVienId.ToString() ?? ""),
                new Claim("nguoiQuanLyCTDTId", nguoiQuanLyCTDTId.ToString() ?? "")
            };
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = await Task.Run(() => tokenHandler.CreateToken(tokenDescriptor));

            return tokenHandler.WriteToken(token);
        }

        public Task<string> GetFullNameAndRole(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Invalid JWT token");
            }
            var jwtToken = handler.ReadJwtToken(token);
            var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "fullname")?.Value ?? throw new ArgumentNullException("fullname", "Full name claim cannot be null.");
        
            return Task.FromResult(fullName);
        }
    }
}