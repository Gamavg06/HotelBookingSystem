using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelReservation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class PanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
