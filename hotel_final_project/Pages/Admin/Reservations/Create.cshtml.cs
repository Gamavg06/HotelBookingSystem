using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelReservation.Data;
using HotelReservation.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Pages.Admin.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly HotelDbContext _db;

        public CreateModel(HotelDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();

        public SelectList RoomList { get; set; } = default!;

        public void OnGet()
        {
            RoomList = new SelectList(_db.Rooms.ToList(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                RoomList = new SelectList(_db.Rooms.ToList(), "Id", "Name");
                return Page();
            }

            _db.Reservations.Add(Reservation);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
