using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    [Authorize(Roles ="President")]
    public class ScheduledMatchesModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IClubServices _clubServices;
        public ScheduledMatchesModel(IMatchRequestServices matchRequestServices, IClubServices clubServices)
        {
            _matchRequestServices = matchRequestServices;
            _clubServices = clubServices;
        }

        [BindProperty]
        public List<MatchRequest> MatchRequests { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var club = await _clubServices.GetClubByPresidentIdAsync(userIdString);

            var matches = await _matchRequestServices.GetPlannedMatchByClubAsync(club.Data);

            MatchRequests = matches.Data;// przypisanie wartoœci do bindProp
            if(MatchRequests == null)
            {
                MatchRequests = new List<MatchRequest>();
            }
            return Page();
        }
    }
}
