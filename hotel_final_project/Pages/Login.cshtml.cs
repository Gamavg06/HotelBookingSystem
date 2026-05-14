using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace HotelReservation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HotelDbContext _db;

        public LoginModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public LoginInput Input { get; set; }

        public string ErrorMessage { get; set; }

        public class LoginInput
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Buscar usuario en EF
            var user = await _db.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == Input.Email &&
                    u.Password == Input.Password
                );

            if (user == null)
            {
                ErrorMessage = "Credenciales incorrectas.";
                return Page();
            }

            // Si el correo es el del admin, forzar rol "Admin"
            var role = user.Role; // 👈 usa el valor real guardado en la base

            // Crear lista de Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, "AuthCookie");
            var principal = new ClaimsPrincipal(identity);

            // Guardar cookie de autenticación
            await HttpContext.SignInAsync("AuthCookie", principal);

            // Redirigir según rol
            if (role == "Admin")
                return RedirectToPage("/Admin/Panel");  // 👈 tu página de administración
            else
                return RedirectToPage("/Index");  // 👈 usuarios normales
        }
    }
}
