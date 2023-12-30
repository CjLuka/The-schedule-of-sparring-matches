using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class PlanMatchWithoutPitchModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IBranchClubServices _branchClubServices;
        public PlanMatchWithoutPitchModel(IMatchRequestServices matchRequestServices, IBranchClubServices branchClubServices)
        {
            _matchRequestServices = matchRequestServices;
            _branchClubServices = branchClubServices;
        }

        [BindProperty]
        public MatchRequest matchRequest { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego u¿ytkownika

            var myBranch = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobieranie branchClubu zalogowanego usera, aby podaæ w parametrze metody id klubu i nie braæ pod uwagê podczas szukania przeciwnika

            //matchRequest.IsAccepted= false;
            matchRequest.SenderId = myBranch.Data.Id;
            matchRequest.ReceiverId = id;
            matchRequest.CreatedBy= userIdString;
            matchRequest.CreatedDate = DateTime.Now;

            await _matchRequestServices.PlanNewMatchAsync(matchRequest);
            return RedirectToPage("../Coach/ScheduledMatches");
        }
    }
}
