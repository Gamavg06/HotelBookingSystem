using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Linq;
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
        public Reservation Reservation { get; set; }

        public SelectList RoomList { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Reservation = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (Reservation == null)
                return RedirectToPage("Index");

            RoomList = new SelectList(_db.Rooms.ToList(), "Id", "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                RoomList = new SelectList(_db.Rooms.ToList(), "Id", "Name");
                return Page();
            }

            _db.Reservations.Update(Reservation);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
