using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // Necesario para FirstOrDefaultAsync()
using HotelReservation.Data;
using HotelReservation.Models;
using System.Linq; // Necesario para FirstOrDefault()

namespace HotelReservation.Pages
{
    public class ConfirmModel : PageModel
    {
        // 1. Cambiado de InMemoryStore a HotelDbContext
        private readonly HotelDbContext _context; 
        
        public Room Room { get; set; }
        public string PaymentMethod { get; set; }
        public string Titular { get; set; }

        // 2. Cambiado el constructor para inyectar HotelDbContext
        public ConfirmModel(HotelDbContext context)
        {
            _context = context;
        }

        // Se usa async Task<IActionResult> para poder usar FirstOrDefaultAsync()
        public async Task OnGetAsync() 
        {
            // Se usa el método TryParse para obtener el ID de la sesión
            if (int.TryParse(HttpContext.Session.GetString("RoomId"), out var roomId))
            {
                // 3. Cambiado el acceso a datos para usar Entity Framework Core (DbContext)
                // Se usa FirstOrDefaultAsync para evitar errores si no se encuentra la habitación
                Room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId); 
            }

            PaymentMethod = HttpContext.Session.GetString("PaymentMethod") ?? "N/A";
            Titular = HttpContext.Session.GetString("CardName") ?? HttpContext.Session.GetString("PayPalEmail") ?? "N/A";
        }
    }
}