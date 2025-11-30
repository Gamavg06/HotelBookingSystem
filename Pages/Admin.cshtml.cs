using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelReservation.Pages
{
    [Authorize(Roles = "Admin")] // 👈 Solo accesible para el rol Admin
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
            // Aquí podrías cargar datos de la BD para mostrar en el panel
        }
    }
}
