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
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string Nationality { get; set; }
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
                ModelState.AddModelError("", "This email is already registered.");
                return Page();
            }

            // Crear usuario con los campos reales
            var user = new User
            {
                Name = Input.Name,
                LastName = Input.LastName,
                Email = Input.Email,
                Password = Input.Password,
                Role = "User", // Rol por defecto
                Nationality = Input.Nationality,
                Age = Input.Age,
                Telephone = Input.Telephone
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return RedirectToPage("Login");
        }
    }
}
