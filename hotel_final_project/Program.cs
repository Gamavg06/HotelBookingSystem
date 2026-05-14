using HotelReservation.Data;
using HotelReservation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// DB Connection
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();

// Registrar EmailService en DI
builder.Services.AddTransient<IEmailService, EmailService>();
// Autenticación con cookies
builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", options =>
    {
        options.LoginPath = "/Access";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorization();

// Razor Pages y HttpContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

// --- SESIÓN (necesario) ---
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// ----------------------------

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

app.UseAuthentication();
app.UseAuthorization();

// **Habilitar sesión antes de MapRazorPages**
app.UseSession();

app.MapRazorPages();

app.Run();
