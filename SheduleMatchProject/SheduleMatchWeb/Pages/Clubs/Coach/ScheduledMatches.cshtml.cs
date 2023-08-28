using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    [Authorize(Roles ="Coach")]
    public class ScheduledMatchesModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        public ScheduledMatchesModel(IMatchRequestServices matchRequestServices)
        {
            _matchRequestServices = matchRequestServices;
        }

        [BindProperty]
        public List<MatchRequest> MatchRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var matches = await _matchRequestServices.GetPlannedMatchByCoachAsync(userIdString);

            MatchRequests = matches.Data;// przypisanie wartoœci do bindProp
            return Page();
        }
    }
}
