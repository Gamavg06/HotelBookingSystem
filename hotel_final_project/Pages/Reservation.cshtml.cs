using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HotelReservation.Data;
using HotelReservation.Models;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Pages
{
    public class ReservationModel : PageModel
    {
        private readonly HotelDbContext _context;
        private readonly ILogger<ReservationModel> _logger;

        public ReservationModel(HotelDbContext context, ILogger<ReservationModel> logger)
        {
            _context = context;
            _logger = logger;
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

        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (SelectedRoom == null)
            {
                return RedirectToPage("/Rooms");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int roomId)
        {
            // Validación básica del modelo
            if (Input == null)
            {
                ModelState.AddModelError("", "Invalid reservation data.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (SelectedRoom == null)
            {
                return RedirectToPage("/Rooms");
            }

            try
            {
                // Usar sesión (ya registrada en Program.cs)
                HttpContext.Session.SetString("RoomId", roomId.ToString());
                HttpContext.Session.SetString("DateIn", Input.DateIn ?? string.Empty);
                HttpContext.Session.SetString("DateOut", Input.DateOut ?? string.Empty);
                HttpContext.Session.SetString("Guests", Input.Guests.ToString());

                // Aquí podrías crear la entidad Reservation y guardarla si lo deseas
                // var reservation = new Reservation { RoomId = roomId, ... };
                // _context.Reservations.Add(reservation);
                // await _context.SaveChangesAsync();

                return RedirectToPage("/Payment");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing reservation for room {RoomId}", roomId);
                ModelState.AddModelError("", "An error occurred while processing the reservation. Please try again.");
                return Page();
            }
        }
    }
}
