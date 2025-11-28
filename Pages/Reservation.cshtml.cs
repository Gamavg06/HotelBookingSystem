using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class ReservationModel : PageModel
    {
        private readonly InMemoryStore _store;

        public ReservationModel(InMemoryStore store)
        {
            _store = store;
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

        public IActionResult OnGet(int roomId)
        {
            SelectedRoom = _store.Rooms.FirstOrDefault(r => r.Id == roomId);

            if (SelectedRoom == null)
            {
                return RedirectToPage("/Rooms");
            }

            return Page();
        }

        public IActionResult OnPost(int roomId)
        {
            SelectedRoom = _store.Rooms.FirstOrDefault(r => r.Id == roomId);

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
