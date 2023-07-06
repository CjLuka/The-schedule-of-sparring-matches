using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SheduleMatchWeb.Pages.Matches
{
    public class PlanNewMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        private readonly IFootballPitchServices _footballPitchServices;
        
        public PlanNewMatchModel(IMatchServices matchServices, IClubServices clubServices, IFootballPitchServices footballPitchServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
            _footballPitchServices= footballPitchServices;
        }

        [BindProperty]
        public MatchRequest NewMatchRequest { get; set; }
        [BindProperty]
        public List<SelectListItem> Clubs { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            List<SelectListItem> ClubsFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla klubów
            List<SelectListItem> FootballPitchFromBase = new List<SelectListItem>();//Utworzenie selectlisty dla stadionow
            var AllClubs = await _clubServices.GetAllAsync();//Pobranie wszystkich druzyn

            foreach (var item in AllClubs.Data)
            {
                ClubsFromBase.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich klubów do selectlisty
            }
            ViewData["AllClubs"] = ClubsFromBase;//przypisanie klubów do ViewData

            var allFootballPitch = await _footballPitchServices.GetAllFootballPitchesAsync();
            foreach (var item in allFootballPitch.Data)
            {
                FootballPitchFromBase.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich stadionow do selectlisty
            }
            ViewData["AllFootballPitch"] = FootballPitchFromBase;//przypisanie stadionów do ViewData
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            NewMatchRequest.IsAccepted = false;//przypisanie braku zaakceptowania meczu podczas tworzenia zapytania o mecz
            return RedirectToPage("/MatchPlanned");
        }
    }
}
