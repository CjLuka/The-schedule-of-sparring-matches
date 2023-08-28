using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class MatchPropositionsModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;

        public MatchPropositionsModel(IMatchRequestServices matchRequestServices)
        {
            _matchRequestServices = matchRequestServices;
        }

        public List<MatchRequest> MatchRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var allPropositions = await _matchRequestServices.GetPropositionsByCoachAsync(userIdString);
            MatchRequests = allPropositions.Data;
            return Page();
        }
    }
}
