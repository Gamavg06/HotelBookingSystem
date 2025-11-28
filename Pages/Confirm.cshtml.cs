using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly InMemoryStore _store;
        public Room Room { get; set; }
        public string PaymentMethod { get; set; }
        public string Titular { get; set; }

        public ConfirmModel(InMemoryStore store)
        {
            _store = store;
        }

        public void OnGet()
        {
            if (int.TryParse(HttpContext.Session.GetString("RoomId"), out var roomId))
            {
                Room = _store.Rooms.FirstOrDefault(r => r.Id == roomId);
            }

            PaymentMethod = HttpContext.Session.GetString("PaymentMethod") ?? "N/A";
            Titular = HttpContext.Session.GetString("CardName") ?? HttpContext.Session.GetString("PayPalEmail") ?? "N/A";
        }
    }
}
