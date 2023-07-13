using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Matches
{
    public class PlanNewMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IBranchClubServices _branchClubServices;
        
        public PlanNewMatchModel(IMatchServices matchServices, IClubServices clubServices, IFootballPitchServices footballPitchServices, IBranchClubServices branchClubServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
            _footballPitchServices= footballPitchServices;
            _branchClubServices= branchClubServices;
        }

        [BindProperty]
        public MatchRequest NewMatchRequest { get; set; }
        [BindProperty]
        public List<SelectListItem> Clubs { get; set; }
        [BindProperty]
        public List<BranchClub> Branches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby móc pobraæ odpowiedni klub
            int.TryParse(userIdString, out int userId);//przerobieine userId na int

            List<SelectListItem> BranchClubsFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla wszystkich zespo³ów
            List<SelectListItem> FootballPitchFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla stadionow
            var AllBranchClubs = await _branchClubServices.GetAllBranchClubsForPlanMatchAsync(userId);//Pobranie wszystkich 

            Branches = AllBranchClubs.Data;
            //foreach (var item in AllBranchClubs.Data)
            //{
            //    BranchClubsFromBase.Add(new SelectListItem { Text = item.Club.Name + " - " + item.Type, Value = item.Id.ToString() });//przypisanie wszystkich klubów do selectlisty
            //}
            //ViewData["AllClubs"] = BranchClubsFromBase;//przypisanie klubów do ViewData

            //var allFootballPitch = await _footballPitchServices.GetAvailableFootballPitchesForMatchRequest(NewMatchRequest.Date);
            //var allFootballPitch = await _footballPitchServices.GetAllFootballPitchesAsync();
            //foreach (var item in allFootballPitch.Data)
            //{
            //    FootballPitchFromBase.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich stadionow do selectlisty
            //}
            //ViewData["AllFootballPitch"] = FootballPitchFromBase;//przypisanie stadionów do ViewData
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            NewMatchRequest.IsAccepted = false;//przypisanie braku zaakceptowania meczu podczas tworzenia zapytania o mecz
            return RedirectToPage("/MatchPlanned");
        }
    }
}
