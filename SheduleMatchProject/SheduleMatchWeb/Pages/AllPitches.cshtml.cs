using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
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
        public ListPaginated<FootballPitch> FootballPitches { get; set; }
        [BindProperty]
        public ModelPagination Pagination { get; set; }
        public async Task<IActionResult> OnGetAsync(int side = 1, int size = 2)
        {
            Pagination = new ModelPagination(side, size);
            var allPitches = await _footballPitchServices.GetAllFootballPitchesAsync(Pagination);

            FootballPitches = allPitches.Data;
            return Page();
        }

    }
}
