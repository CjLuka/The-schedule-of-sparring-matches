using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace SheduleMatchWeb.Pages.Clubs.Coach
{
    public class ChosePitchForMatchModel : PageModel
    {
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IMatchRequestServices _matchRequestServices;

        public ChosePitchForMatchModel(IFootballPitchServices footballPitchServices, IMatchRequestServices matchRequestServices)
        {
            _footballPitchServices = footballPitchServices;
            _matchRequestServices = matchRequestServices;
        }

        [BindProperty]
        public MatchRequest MatchRequest { get; set; }
        [BindProperty]
        public List<FootballPitch> FootballPitches { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MatchRequestData { get; set; } // Przechowuje dane matchRequest w adresie URL

        public async Task<IActionResult> OnGetAsync(/*int id*/)
        {
            if (!string.IsNullOrEmpty(MatchRequestData))
            {
                var matchRequest = JsonConvert.DeserializeObject<MatchRequest>(MatchRequestData);
                MatchRequest = matchRequest;


                var availableFootballPitches = await _footballPitchServices.GetAvailableFootballPitchesForMatchRequest(matchRequest.Date);
                FootballPitches = availableFootballPitches.Data.ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int footballPitchId)
        {

            var matchRequest = JsonConvert.DeserializeObject<MatchRequest>(MatchRequestData);//przekonwertowanie z jsona z powrotem na obiekt
            MatchRequest = matchRequest;
            MatchRequest.FootballPitchId = footballPitchId;
            try
            {
                var match = await _matchRequestServices.PlanNewMatchAsync(MatchRequest);
                return RedirectToPage("./ScheduledMatches");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
