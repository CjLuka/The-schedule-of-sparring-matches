using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        public UpdateBranchModel(IBranchClubServices branchClubServices, IUserServices userServices, UserManager<User> userManager)
        {
            _branchClubServices = branchClubServices;
            _userServices = userServices;
            _userManager = userManager;
        }
        [BindProperty]
        public BranchClub BranchClub { get; set; }
        public static string previousCoachId;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var myBranch = await _branchClubServices.GetDetailBranchByIdAsync(id);//pobranie danych na temat zespo³u po id
            BranchClub = myBranch.Data;

            var currentlyCoach = await _userServices.GetUserById(BranchClub.UserId);//pobranie obecnego trenera
            previousCoachId = currentlyCoach.Data.Id;//przypisanie id obecnego trenera

            List<SelectListItem> Coaches = new List<SelectListItem>();//Utworzenie selectlisty dla trenerow bez klubu

            //var coachesFromBase = await _userServices.GetCoaches();//pobranie uzytkownikow
            var coachesFromBase = await _userServices.GetUsersWithoutAnyFunction();//pobranie uzytkownikow
            foreach (var user in coachesFromBase.Data)
            {
                Coaches.Add(new SelectListItem { Text = user.FirstName + " " + user.LastName, Value = user.Id.ToString() });//dodanie uzytkownikow, ktorzy nie sa prezesami zadnego klubu do selectlisty
            }
            var AllCoaches = await _userServices.GetAllCoaches();//pobranie wszystkich uzytkownikow
            //var AllCoaches = await _userServices.GetUsersWithoutAnyFunction();//pobranie wszystkich uzytkownikow
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
                //var myBranch = await _branchClubServices.GetDetailBranchByIdAsync(id);//pobranie danych na temat zespo³u po id
                //BranchClub = myBranch.Data;

                string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
                
                BranchClub.LastModifiedBy = userEmail;
                BranchClub.LastModifiedDate= DateTime.Now;
                BranchClub.CreatedBy = userEmail;

                await _branchClubServices.UpdateBranchAsync(BranchClub, id);

                if (previousCoachId != BranchClub.UserId)//w przypadku zmiany trenera - usuniêcie roli staremu u¿ytkownikowi(rola Coach)
                {
                    //W przypadku zmiany, pobranie obu u¿ytkowników i przypisanie im odpowiednich ról
                    var oldPresident = await _userServices.GetUserById(previousCoachId);
                    var newPresident = await _userServices.GetUserById(BranchClub.UserId);

                    //var countClubs = await _branchClubServices.CountBranchesForCoach(oldPresident.Data.Id);

                    //if(countClubs.Data == 0)
                    //{
                    //    await _userManager.RemoveFromRoleAsync(oldPresident.Data, "Coach");
                    //}
                    
                    await _userManager.AddToRoleAsync(newPresident.Data, "Coach");
                    
                }

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
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var deleted = await _branchClubServices.DeleteBranchAsync(id);
            if (deleted.Success)
            {
                var oldPresident = await _userServices.GetUserById(previousCoachId);//pobranie u¿ytkownika który by³ trenerem oddzialu
                await _userManager.RemoveFromRoleAsync(oldPresident.Data, "Coach");//usuniêcie roli trenera przy usuniêciu oddzialu
                return RedirectToPage("../ShowAllClubs");
            }
            return Page();
        }
    }
}
