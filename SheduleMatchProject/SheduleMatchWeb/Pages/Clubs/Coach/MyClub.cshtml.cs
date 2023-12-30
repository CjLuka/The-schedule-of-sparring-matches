using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class MyClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;
        
        public MyClubModel(IClubServices clubServices, IBranchClubServices branchClubServices, IUserServices userServices)
        {
            _clubServices = clubServices;
            _branchClubServices= branchClubServices;
            _userServices = userServices;
        }
        [BindProperty]
        public BranchClub Club { get; set; }
        [BindProperty]
        public string EmailPresident { get; set; }
        public async Task <IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var myClub = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);
            var email = await _userServices.GetEmailByUserIdAsync(myClub.Data.Club.UserId);
            
            //przypisanie danych do bindProperty
            Club = myClub.Data;
            EmailPresident = email;

            return Page();
        }
    }
}
