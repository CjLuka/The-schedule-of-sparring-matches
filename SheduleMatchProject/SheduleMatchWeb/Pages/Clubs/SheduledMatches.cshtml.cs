using Aplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class SheduledMatchesModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;

        public SheduledMatchesModel(IMatchRequestServices matchRequestServices)
        {
            _matchRequestServices = matchRequestServices;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //var mySheduledMatchese = _matchRequestServices.GetPlannedMatchAsync();
            return Page();
        }
    }
}
