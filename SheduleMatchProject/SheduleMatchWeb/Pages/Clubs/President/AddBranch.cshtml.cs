using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    public class AddBranchModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;
        private readonly IClubServices _clubServices;
        public AddBranchModel(IBranchClubServices branchClubServices, IUserServices userServices, IClubServices clubServices)
        {
            _branchClubServices = branchClubServices;
            _userServices = userServices;
            _clubServices = clubServices;

        }

        [BindProperty]
        public BranchClub Branch { get; set; }
        [BindProperty] 
        public Club Club { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            List<SelectListItem> Coaches = new List<SelectListItem>();//Utworzenie selectlisty dla trenerow bez klubu

            string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby wyswietlic odpowiedni klub
            int.TryParse(userIdString, out int userId);//przerobieine userId na int

            var myClub = await _clubServices.GetClubByPresidentIdAsync(userId);
            Club = myClub.Data;

            var coachesFromBase = await _userServices.GetCoachesWithoutClub();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
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

            Branch.CreatedDate= DateTime.Now;
            Branch.CreatedBy = userEmail;
            Branch.ClubId = Club.Id;
            await _branchClubServices.AddBranchAsync(Branch);

            return RedirectToAction("/President/MyBranches");
        }
    }
}
