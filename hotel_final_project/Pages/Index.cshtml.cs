using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservation.Pages
{
    [Authorize]   // 🔒 Requiere login
    public class IndexModel : PageModel
    {
        private readonly HotelDbContext _db;

        public List<Room> Rooms { get; set; } = new();

        public IndexModel(HotelDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Rooms = _db.Rooms.ToList();
        }
    }
}
