using Microsoft.EntityFrameworkCore;
using be_company.Data;  // Đảm bảo khai báo đúng namespace của AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CORS để cho phép yêu cầu từ React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // Địa chỉ của React App
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

// Áp dụng chính sách CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Cấu hình các route controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
