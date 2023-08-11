using Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class PlanMatchWithPitchModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IFootballPitchServices _footballPitchServices;
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            return RedirectToPage("/BranchesForPlanMatch");
        }
    }
}
