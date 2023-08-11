using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    [Authorize(Roles = "Admin, President")]
    public class AddBranchModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;
        private readonly IClubServices _clubServices;
        private readonly UserManager<User> _userManager;
        public AddBranchModel(IBranchClubServices branchClubServices, IUserServices userServices, IClubServices clubServices, UserManager<User> userManager)
        {
            _branchClubServices = branchClubServices;
            _userServices = userServices;
            _clubServices = clubServices;
            _userManager= userManager;
        }

        [BindProperty]
        public BranchClub Branch { get; set; }
        [BindProperty] 
        public Club Club { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            List<SelectListItem> Coaches = new List<SelectListItem>();//Utworzenie selectlisty dla trenerow bez klubu

            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby wyswietlic odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int

            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);

            var myClub = await _clubServices.GetClubByPresidentIdAsync(userIdString);
            Club = myClub.Data;

            var coachesFromBase = await _userServices.GetCoaches();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
            //var coachesFromBase = await _userServices.GetCoachesWithoutClub();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
            foreach (var user in coachesFromBase.Data)
            {
                Coaches.Add(new SelectListItem { Text = user.FirstName + " " + user.LastName, Value = user.Id.ToString() });//dodanie uzytkownikow, ktorzy nie sa prezesami zadnego klubu do selectlisty
            }
            ViewData["Coaches"] = Coaches;//przypisanie listy uzytkownikow do ViewData
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);

            var myClub = await _clubServices.GetClubByPresidentIdAsync(userIdString);
            Branch.CreatedDate= DateTime.Now;
            Branch.CreatedBy = userEmail;
            Branch.ClubId = myClub.Data.Id;

            await _branchClubServices.AddBranchAsync(Branch);
            var newCoach = await _userServices.GetUserById(Branch.UserId);//pobranie u¿ytkownika, który bêdzie nowym trenerem
            var addRolesResult = await _userManager.AddToRoleAsync(newCoach.Data, "Coach");//przypisanie roli trenera nowemu trenerowi

            return RedirectToPage("../President/MyBranches");
        }
    }
}
