using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.BranchClubs
{
    public class MyBranchClubModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        public MyBranchClubModel(IBranchClubServices branchClubServices)
        {
            _branchClubServices = branchClubServices;
        }
        [BindProperty]
        public BranchClub MyBranchClub { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby wyswietlic odpowiedni zesp�
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int
            
            ClaimsPrincipal currentUser = this.User;//pobranie u�ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var BranchClubFromBase = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);
            MyBranchClub = BranchClubFromBase.Data;

            return Page();
        }
    }
}
