using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace HotelReservation.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // Cierra la sesión y elimina la cookie
            await HttpContext.SignOutAsync("AuthCookie");

            // Redirige al login
            return RedirectToPage("/Login");
        }
    }
}
