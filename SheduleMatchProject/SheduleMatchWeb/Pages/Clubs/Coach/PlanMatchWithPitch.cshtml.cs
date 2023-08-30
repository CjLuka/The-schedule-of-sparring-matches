using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class PlanMatchWithPitchModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IBranchClubServices _branchClubServices;

        public PlanMatchWithPitchModel(IMatchRequestServices matchRequestServices, IFootballPitchServices footballPitchServices, IBranchClubServices branchClubServices)
        {
            _matchRequestServices = matchRequestServices;
            _footballPitchServices = footballPitchServices;
            _branchClubServices = branchClubServices;
        }
        [BindProperty]
        public MatchRequest matchRequest { get; set; }
        //public BranchClub branchClub { get; set; }
        //[BindProperty(SupportsGet = true)]
        public string branchRequestData { get; set; } // Przechowuje dane w adresie URL
        public async Task<IActionResult> OnGetAsync(int id)
        {
            //await _footballPitchServices.GetAvailableFootballPitchesForMatchRequest(matchRequest.Date);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u�ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;//pobranie Id zalogowanego u�ytkownika

            var myBranch = await _branchClubServices.GetBranchClubByCoachAsync(userIdString);//pobieranie branchClubu zalogowanego usera, aby poda� w parametrze metody id klubu i nie bra� pod uwag� podczas szukania przeciwnika

            //var branchRequest = JsonConvert.DeserializeObject<BranchClub>(branchRequestData);
            //branchClub = branchRequest;

            //matchRequest.IsAccepted = false;
            matchRequest.SenderId = myBranch.Data.Id;
            //matchRequest.SenderId = branchClub.Id;
            matchRequest.ReceiverId = id;
            matchRequest.CreatedBy = userIdString;
            matchRequest.CreatedDate = DateTime.Now;  
            try
            {
                var serializedMatchRequest = JsonConvert.SerializeObject(matchRequest);//konwertowanie obiektu na jsona, aby m�c go przes�a� do innego widoku
                return RedirectToPage("./ChosePitchForMatch", new { matchRequestData = serializedMatchRequest });//przes�aie obiektu przez json
            }
            catch (Exception)
            {
                throw;
            }             
        }
    }
}
