using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class ChosePitchForMatchModel : PageModel
    {
        [BindProperty]
        public MatchRequest MatchRequest { get; set; }
        public async Task<IActionResult> OnGetAsync(string matchRequestData)
        {
            var matchRequest = JsonConvert.DeserializeObject<MatchRequest>(matchRequestData);
            MatchRequest = matchRequest;
            return Page();
        }
    }
}
