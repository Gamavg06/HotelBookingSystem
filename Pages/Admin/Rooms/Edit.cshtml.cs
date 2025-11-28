using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages.Admin.Rooms
{
    public class EditModel : PageModel
    {
        private readonly HotelDbContext _db;

        public EditModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task OnGet(int id)
        {
            Room = await _db.Rooms.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _db.Rooms.Update(Room);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
