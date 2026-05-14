using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages.Admin.Reservations
{
    public class DetailsModel : PageModel
    {
        private readonly HotelDbContext _context;

        public DetailsModel(HotelDbContext context)
        {
            _context = context;
        }

        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Reservation == null)
                return NotFound();

            return Page();
        }
    }
}
