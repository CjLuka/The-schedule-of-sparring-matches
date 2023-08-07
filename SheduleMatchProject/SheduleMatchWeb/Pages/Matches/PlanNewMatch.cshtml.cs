using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Matches
{
    [Authorize(Roles = "Coach")]
    public class PlanNewMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;
        
        public PlanNewMatchModel(IMatchServices matchServices, IClubServices clubServices,
            IFootballPitchServices footballPitchServices, IBranchClubServices branchClubServices, IUserServices userServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
            _footballPitchServices= footballPitchServices;
            _branchClubServices= branchClubServices;
            _userServices = userServices;
        }

        [BindProperty]
        public MatchRequest NewMatchRequest { get; set; }
        [BindProperty]
        public List<SelectListItem> Clubs { get; set; }
        [BindProperty]
        public List<BranchClub> Branches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego u¿ytkownika


            var branch = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobieranie branchClubu zalogowanego usera, aby podaæ w parametrze metody id klubu i nie braæ pod uwagê podczas szukania przeciwnika


            List<SelectListItem> BranchClubsFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla wszystkich zespo³ów
            List<SelectListItem> FootballPitchFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla stadionow

            var AllBranchClubs = await _branchClubServices.GetAllBranchClubsForPlanMatchAsync(branch.Data.ClubId);//Pobranie wszystkich 

            Branches = AllBranchClubs.Data;
            return Page();
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    NewMatchRequest.IsAccepted = false;//przypisanie braku zaakceptowania meczu podczas tworzenia zapytania o mecz
        //    return RedirectToPage("/MatchPlanned");
        //}
    }
}
