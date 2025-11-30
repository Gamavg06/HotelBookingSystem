using HotelReservation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// DB Connection
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticación con cookies
builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", options =>
    {
        options.LoginPath = "/Access";          // Redirige aquí si no está logeado
        options.AccessDeniedPath = "/AccessDenied"; // Página de acceso denegado
    });

builder.Services.AddAuthorization();

// Razor Pages
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // 🔒 Habilita autenticación
app.UseAuthorization();   // 🔒 Habilita autorización

app.MapRazorPages();

app.Run();
