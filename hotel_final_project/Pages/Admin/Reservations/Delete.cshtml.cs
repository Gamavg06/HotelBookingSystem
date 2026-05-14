using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Pages.Admin.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly HotelDbContext _db;

        public DeleteModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _db.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Reservation == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var res = await _db.Reservations.FindAsync(Reservation.Id);

            if (res != null)
            {
                _db.Reservations.Remove(res);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
