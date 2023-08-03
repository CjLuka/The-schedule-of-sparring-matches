using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    public class MyBranchesModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        private readonly IClubServices _clubServices;
        
        public MyBranchesModel(IBranchClubServices branchClubServices, IClubServices clubServices)
        {
            _branchClubServices = branchClubServices;
            _clubServices = clubServices;
        }


        [BindProperty]
        public List<BranchClub> BranchClubs{ get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby wyswietlic odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int

            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);

            var club = await _clubServices.GetClubByPresidentIdAsync(userIdString);//pobranie klubu zalogowanego uzytkownika

            var myBranches = await _branchClubServices.GetBranchesByClubAsync(club.Data.Id);//Pobranie wszystkich zespo³ów przypisanych do klubu
            BranchClubs = myBranches.Data;
            return Page();
        }
    }
}
