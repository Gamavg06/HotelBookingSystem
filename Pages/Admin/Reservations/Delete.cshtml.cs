using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Threading.Tasks;

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
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Reservation = await _db.Reservations.FindAsync(id);

            if (Reservation == null)
                return RedirectToPage("Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            var res = _db.Reservations.Find(Reservation.Id);

            if (res != null)
            {
                _db.Reservations.Remove(res);
                _db.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
