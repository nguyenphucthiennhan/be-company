using Microsoft.EntityFrameworkCore;
using be_company.Data;  // Ensure this namespace is correct for your AppDbContext

var builder = WebApplication.CreateBuilder(args);

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  // Your React App origin
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// DbContext configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware configuration
app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS policy
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Configure controllers
app.MapControllers();  // This will handle attribute-based routing for API controllers

app.Run();
