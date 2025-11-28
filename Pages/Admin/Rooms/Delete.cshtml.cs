using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages.Admin.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly HotelDbContext _db;

        public DeleteModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Room Room { get; set; }

        public async Task OnGet(int id)
        {
            Room = await _db.Rooms.FindAsync(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var room = await _db.Rooms.FindAsync(id);

            if (room != null)
            {
                _db.Rooms.Remove(room);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
