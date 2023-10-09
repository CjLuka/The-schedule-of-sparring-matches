using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    public class MatchesPlayedModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        public MatchesPlayedModel(IMatchServices matchServices, IClubServices clubServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
        }

        [BindProperty]
        public List<Match> Matches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego uzytkownika
            var club = await _clubServices.GetClubByPresidentIdAsync(userId);

            var allMatches = await _matchServices.GetAllByClubAsync(club.Data.Id);
            Matches = allMatches.Data;
            return Page();
        }
    }
}
