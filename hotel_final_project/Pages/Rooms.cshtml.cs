using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // ¡NUEVO! Necesario para ToListAsync
using HotelReservation.Data;
using HotelReservation.Models;

namespace HotelReservation.Pages
{
    public class RoomsModel : PageModel
    {
        // CAMBIO 1: Reemplazar InMemoryStore por HotelDbContext
        private readonly HotelDbContext _context; 
        
        public List<Room> Rooms { get; set; } = new();

        // CAMBIO 2: Actualizar el constructor
        public RoomsModel(HotelDbContext context) { _context = context; }
        
        // CAMBIO 3: Hacer el método asíncrono y usar ToListAsync
        public async Task OnGetAsync() 
        { 
            Rooms = await _context.Rooms.ToListAsync(); 
        }
    }
}