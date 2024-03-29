using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class MyPlannedMatchModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IBranchClubServices _branchClubServices;
        public MyPlannedMatchModel(IMatchRequestServices matchRequestServices, IBranchClubServices branchClubServices)
        {
            _matchRequestServices = matchRequestServices;
            _branchClubServices = branchClubServices;
        }

        [BindProperty]
        public List<MatchRequest> matchRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby m�c pobra� odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int

            ClaimsPrincipal currentUser = this.User;//pobranie u�ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);

            var branchClub = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobranie zespo�u poprzez id trenera
            var matchRequeestsFromBase = await _matchRequestServices.GetPlannedMatchByBranchAsync(branchClub.Data);//pobranie wszystkich mecz�w per klub
            matchRequests = matchRequeestsFromBase.Data;

            return Page();
        }
    }
}
