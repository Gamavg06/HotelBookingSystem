using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class RoomsModel : PageModel
    {
        private readonly InMemoryStore _store;
        public List<Room> Rooms { get; set; } = new();
        public RoomsModel(InMemoryStore store) { _store = store; }
        public void OnGet() { Rooms = _store.Rooms; }
    }
}
