using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class MyClubModel : PageModel
    {
        public async Task <IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}
