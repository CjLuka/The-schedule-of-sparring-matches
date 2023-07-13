using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SheduleMatchWeb.Pages.Clubs.President
{
    public class UpdateBranchModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;
        public UpdateBranchModel(IBranchClubServices branchClubServices, IUserServices userServices)
        {
            _branchClubServices = branchClubServices;
            _userServices = userServices;
        }
        [BindProperty]
        public BranchClub BranchClub { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var myBranch = await _branchClubServices.GetDetailBranchByIdAsync(id);//pobranie danych na temat zespo³u po id
            BranchClub = myBranch.Data;

            List<SelectListItem> Coaches = new List<SelectListItem>();//Utworzenie selectlisty dla trenerow bez klubu

            var coachesFromBase = await _userServices.GetCoachesWithoutClub();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
            foreach (var user in coachesFromBase.Data)
            {
                Coaches.Add(new SelectListItem { Text = user.FirstName + " " + user.LastName, Value = user.Id.ToString() });//dodanie uzytkownikow, ktorzy nie sa prezesami zadnego klubu do selectlisty
            }
            var AllCoaches = await _userServices.GetAllCoaches();//pobranie wszystkich uzytkownikow
            foreach (var user in AllCoaches.Data)
            {
                if (user.Id == BranchClub.UserId)//Dopisanie uzytkownika ktory obecnie jest trenerem zespolu
                {
                    Coaches.Add(new SelectListItem { Text = user.FirstName + " " + user.LastName, Value = user.Id.ToString() });
                }
                continue;
            }
            ViewData["Coaches"] = Coaches;//przypisanie listy uzytkownikow do ViewData
            return Page();
        }
    }
}
