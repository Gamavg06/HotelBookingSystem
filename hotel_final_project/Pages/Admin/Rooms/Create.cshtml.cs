using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages.Admin.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly HotelDbContext _db;

        public CreateModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _db.Rooms.Add(Room);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
