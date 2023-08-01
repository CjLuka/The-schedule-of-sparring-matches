using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Matches
{
    public class MatchPlannedModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IBranchClubServices _branchClubServices;
        public MatchPlannedModel(IMatchRequestServices matchRequestServices, IBranchClubServices branchClubServices)
        {
            _matchRequestServices = matchRequestServices;
            _branchClubServices = branchClubServices;
        }
        [BindProperty]
        public List<MatchRequest> matchRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby móc pobraæ odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int
            
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out var userId);
            var branchClub = await _branchClubServices.GetBranchClubByCoachAsync(userId);//pobranie zespo³u poprzez id trenera
            var matchRequeestsFromBase = await _matchRequestServices.GetPlannedMatchAsync(branchClub.Data);//pobranie wszystkich meczów per klub
            matchRequests = matchRequeestsFromBase.Data;

            return Page();
        }
    }
}
