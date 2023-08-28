using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    [Authorize(Roles ="Coach")]
    public class BranchesForPlanMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IBranchClubServices _branchClubServices;
        private readonly IUserServices _userServices;

        public BranchesForPlanMatchModel(IMatchServices matchServices, IClubServices clubServices, 
            IFootballPitchServices footballPitchServices, IBranchClubServices branchClubServices, IUserServices userServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
            _footballPitchServices = footballPitchServices;
            _branchClubServices = branchClubServices;
            _userServices = userServices;
        }

        [BindProperty]
        public MatchRequest NewMatchRequest { get; set; }
        [BindProperty]
        public List<SelectListItem> Clubs { get; set; }
        [BindProperty]
        public List<BranchClub> Branches { get; set; }
        public BranchClub branchClub { get; set; }
        [BindProperty(SupportsGet = true)]
        public string branchRequestData { get; set; } // Przechowuje dane w adresie URL
        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u�ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego użytkownika

            //sprawdzenie, czy klub użytkownik jest trenerem dla więcej niż jednego zespołu
            var allBranchUser = await _branchClubServices.GetAllBranchesForCoach(userIdString);
            if (allBranchUser.Data.Count > 1)
            {
                return RedirectToPage("./ChooseClub");
            }

            var branch = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobieranie branchClubu zalogowanego usera, aby poda� w parametrze metody id klubu i nie bra� pod uwag� podczas szukania przeciwnika


            var AllBranchClubs = await _branchClubServices.GetAllBranchClubsForPlanMatchAsync(branch.Data.ClubId);//Pobranie wszystkich 

            Branches = AllBranchClubs.Data;
            return Page();



            //ClaimsPrincipal currentUser = this.User;//pobranie użytkownika
            //string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego użytkownika

            //var allBranchUser = await _branchClubServices.GetAllBranchesForCoach(userIdString);

            ////jeśli wartość jest pusta i user posiada więcej niz jeden klub, to przenies do wyboru zespolu
            //if (/*string.IsNullOrEmpty(branchRequestData) && */allBranchUser.Data.Count > 1)
            //{
            //    return RedirectToPage("./ChooseClub");
            //}
            //if (TempData.ContainsKey("SerializedBranchRequest"))
            //{
            //    var serializedBranchRequest = TempData["SerializedBranchRequest"] as string;
            //    branchClub = JsonConvert.DeserializeObject<BranchClub>(serializedBranchRequest);
            //}

            //var branch = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobieranie branchClubu zalogowanego usera, aby podać w parametrze metody id klubu i nie brać pod uwagę podczas szukania przeciwnika

            //var AllBranchClubs = await _branchClubServices.GetAllBranchClubsForPlanMatchAsync(branch.Data.ClubId);//Pobranie wszystkich 

            //Branches = AllBranchClubs.Data;
            //return Page();
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    NewMatchRequest.IsAccepted = false;//przypisanie braku zaakceptowania meczu podczas tworzenia zapytania o mecz
        //    return RedirectToPage("/MatchPlanned");
        //}
    }
}
