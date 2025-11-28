using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservation.Pages.Admin.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly HotelDbContext _db;

        public IndexModel(HotelDbContext db)
        {
            _db = db;
        }

        public List<Reservation> Reservations { get; set; }

        public async Task OnGet()
        {
            Reservations = await _db.Reservations
                .Include(r => r.Room)
                .ToListAsync();
        }
    }
}
