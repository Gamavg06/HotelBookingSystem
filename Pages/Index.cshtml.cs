using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly InMemoryStore _store;

        public List<Room> Rooms { get; set; } = new();

        public IndexModel(InMemoryStore store)
        {
            _store = store;
        }

        public void OnGet()
        {
            Rooms = _store.Rooms;
        }
    }
}
