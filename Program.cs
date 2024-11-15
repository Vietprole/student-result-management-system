using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Allow5500",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5500");
                      });
    options.AddPolicy(name: "Allow5173",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173");
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Allow5500");
app.UseCors("Allow5173");

app.UseAuthorization();

app.MapControllers();

app.Run();
