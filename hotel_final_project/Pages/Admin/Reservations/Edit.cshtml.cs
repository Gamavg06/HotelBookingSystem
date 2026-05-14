using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Threading.Tasks;

namespace HotelReservation.Pages.Admin.Reservations
{
    public class EditModel : PageModel
    {
        private readonly HotelDbContext _db;

        public EditModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public SelectList RoomList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _db.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Reservation == null)
                return RedirectToPage("Index");

            RoomList = new SelectList(await _db.Rooms.ToListAsync(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RoomList = new SelectList(await _db.Rooms.ToListAsync(), "Id", "Name");
                return Page();
            }

            _db.Reservations.Update(Reservation);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
