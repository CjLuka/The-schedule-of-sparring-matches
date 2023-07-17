using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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
        public async Task <IActionResult> OnPostUpdateAsync(int id)
        {
            try
            {
             

                string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
                
                BranchClub.LastModifiedBy = userEmail;
                BranchClub.LastModifiedDate= DateTime.Now;

                await _branchClubServices.UpdateBranchAsync(BranchClub, id, userEmail);
                
                ViewData["Notification"] = new Notification
                {
                    Message = "Rekord poprawnie edytowany",
                    Type = Domain.Models.Enum.NotificationType.Success
                };

                return RedirectToPage("/Clubs/President/MyBranches");
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Coœ posz³o nie tak",
                    Type = Domain.Models.Enum.NotificationType.Error
                };
            }

            return Page();
        }
    }
}
