using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego uzytkownika

            //Napisane w ten sposób, bo wali³o b³¹d z inkrementacj¹ Id z niewiadomcych przyczyn xD
            Match newMatch = new Match();
            newMatch.DateStart = matchRequest.Data.Date;
            newMatch.DateEnd = matchRequest.Data.Date.AddHours(2);
            newMatch.BranchClubHomeId = matchRequest.Data.SenderId;
            newMatch.BranchClubAwayId = matchRequest.Data.ReceiverId;
            newMatch.CreatedDate= DateTime.Now;
            newMatch.CreatedBy = userId;
            newMatch.GoalsAway = Match.GoalsAway;
            newMatch.GoalsHome = Match.GoalsHome;

            newMatch.MatchRequestId = matchRequest.Data.Id;
            newMatch.FootballPitchId = matchRequest.Data.FootballPitchId;


            Match = newMatch;
            await _matchServices.AddAsync(Match);

            matchRequest.Data.HasResult= true;

            //Nadpisanie hasResult dla matchRequest
            await _matchRequestServices.UpdateAsync(matchRequest.Data);
            matchRequest.Data.HasResult= true;

            return RedirectToPage("./ScheduledMatches");
        }
    }
}
