using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages.Admin.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly HotelDbContext _db;

        public IndexModel(HotelDbContext db)
        {
            _db = db;
        }

        public List<Room> RoomList { get; set; }

        public async Task OnGet()
        {
            RoomList = await _db.Rooms.ToListAsync();
        }
    }
}
