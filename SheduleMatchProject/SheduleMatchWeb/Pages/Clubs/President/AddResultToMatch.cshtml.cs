using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    public class AddResultToMatchModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IMatchServices _matchServices;

        public AddResultToMatchModel(IMatchRequestServices matchRequestServices, IMatchServices matchServices)
        {
            _matchRequestServices = matchRequestServices;
            _matchServices = matchServices;
        }

        [BindProperty]
        public MatchRequest MatchRequest { get; set; }
        [BindProperty]
        public Match Match { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var matchRequest = await _matchRequestServices.GetMatchRequestByIdAsync(id);
            MatchRequest = matchRequest.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var matchRequest = await _matchRequestServices.GetMatchRequestByIdAsync(id);
            
            
            //Napisane w ten sposób, bo wali³o b³¹d z inkrementacj¹ Id z niewiadomcych przyczyn xD
            Match newMatch = new Match();
            newMatch.DateStart = matchRequest.Data.Date;
            newMatch.DateEnd = matchRequest.Data.Date.AddHours(2);
            newMatch.BranchClubHomeId = matchRequest.Data.SenderId;
            newMatch.BranchClubAwayId = matchRequest.Data.ReceiverId;
            newMatch.CreatedDate= DateTime.Now;
            newMatch.CreatedBy = "test";
            newMatch.GoalsAway = Match.GoalsAway;
            newMatch.GoalsHome = Match.GoalsHome;

            newMatch.MatchRequestId = matchRequest.Data.Id;
            newMatch.FootballPitch.Id = matchRequest.Data.FootballPitchId;
            if (matchRequest.Data.FootballPitchId == null)
            {
                newMatch.FootballPitch = null;
            }

            Match = newMatch;
            await _matchServices.AddAsync(Match);
            return RedirectToPage("./ScheduledMatches");
        }
    }
}
