using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages
{
    public class AllPitchesModel : PageModel
    {
        private readonly IFootballPitchServices _footballPitchServices;

        public AllPitchesModel(IFootballPitchServices footballPitchServices)
        {
            _footballPitchServices = footballPitchServices;
        }

        [BindProperty]
        public List<FootballPitch> FootballPitches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var allPitches = await _footballPitchServices.GetAllFootballPitchesAsync();

            FootballPitches = allPitches.Data;
            return Page();
        }

    }
}
