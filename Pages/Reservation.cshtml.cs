using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // ¡NUEVO! Necesario para FirstOrDefaultAsync
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class ReservationModel : PageModel
    {
        // CAMBIO 1: Reemplazar InMemoryStore por HotelDbContext
        private readonly HotelDbContext _context;

        // CAMBIO 2: Actualizar el constructor
        public ReservationModel(HotelDbContext context)
        {
            _context = context;
        }

        public Room SelectedRoom { get; set; }

        [BindProperty]
        public ReservationInput Input { get; set; }

        public class ReservationInput
        {
            public string DateIn { get; set; }
            public string DateOut { get; set; }
            public int Guests { get; set; }
        }

        // CAMBIO 3: Hacer el método asíncrono
        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            // CAMBIO 4: Usar FirstOrDefaultAsync
            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (SelectedRoom == null)
            {
                return RedirectToPage("/Rooms");
            }

            return Page();
        }

        // CAMBIO 5: Hacer el método asíncrono
        public async Task<IActionResult> OnPostAsync(int roomId)
        {
            // CAMBIO 6: Usar FirstOrDefaultAsync
            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (SelectedRoom == null)
            {
                return RedirectToPage("/Rooms");
            }

            // Guardar temporalmente en sesión la reservación
            HttpContext.Session.SetString("RoomId", roomId.ToString());
            HttpContext.Session.SetString("DateIn", Input.DateIn);
            HttpContext.Session.SetString("DateOut", Input.DateOut);
            HttpContext.Session.SetString("Guests", Input.Guests.ToString());

            return RedirectToPage("/Payment");
        }
    }
}