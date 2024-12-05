using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Repository;
using Student_Result_Management_System.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<ITokenSerivce, TokenService>();
builder.Services.AddScoped<IChucVuRepository,ChucVuRepository>();
builder.Services.AddScoped<IKhoaRepository,KhoaRepository>();
builder.Services.AddScoped<ISinhVienRepository,SinhVienRepository>();
builder.Services.AddScoped<IGiangVienRepository,GiangVienRepository>();
builder.Services.AddScoped<IKiHocRepository,KiHocRepository>();
builder.Services.AddScoped<IBaiKiemTraRepository,BaiKiemTraRepository>();
builder.Services.AddScoped<ICauHoiRepository,CauHoiRepository>();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<TaiKhoan, IdentityRole>(options => {
        options.Password.RequireDigit = true; // Mật khẩu phải chứa ít nhất 1 số
        options.Password.RequireLowercase = true; // Mật khẩu phải chứa ít nhất 1 chữ thường
        options.Password.RequireNonAlphanumeric = true; // Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt
        options.Password.RequireUppercase = true; // Mật khẩu phải chứa ít nhất 1 chữ hoa
        options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{   
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? throw new InvalidOperationException("JWT:SigningKey is not configured"))),

    };
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
