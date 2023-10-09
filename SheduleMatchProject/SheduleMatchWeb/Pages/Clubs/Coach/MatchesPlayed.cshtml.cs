using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class MatchesPlayedModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IBranchClubServices _branchClubServices;
        public MatchesPlayedModel(IMatchServices matchServices, IBranchClubServices branchClubServices)
        {
            _matchServices = matchServices;
            _branchClubServices = branchClubServices;
        }

        [BindProperty]
        public List<Match> Matches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego uzytkownika
            var branchClub = await _branchClubServices.GetBranchClubByCoachAsync(userId);

            var allMatches = await _matchServices.GetAllByBranchClubAsync(branchClub.Data.Id);
            Matches = allMatches.Data;
            return Page();
        }
    }
}
