using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HotelDbContext _db;

        public RegisterModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public RegisterInput Input { get; set; }

        public class RegisterInput
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Edad { get; set; }
            public string Celular { get; set; }
            public string Email { get; set; }
            public string Nacionalidad { get; set; }
            public string Password { get; set; }
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Validar correo único
            bool exist = await _db.Users.AnyAsync(u => u.Email == Input.Email);

            if (exist)
            {
                ModelState.AddModelError("", "Este correo ya está registrado.");
                return Page();
            }

            // Crear usuario con los campos reales
            var user = new User
            {
                Nombre = Input.Nombre,
                Apellido = Input.Apellido,
                Email = Input.Email,
                Password = Input.Password,
                Role = "User", // Rol por defecto
                Nacionalidad = Input.Nacionalidad,
                Edad = Input.Edad,
                Celular = Input.Celular
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}
