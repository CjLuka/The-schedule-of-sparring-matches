using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleMatchWeb.Pages.Matches
{
    public class AllMatchesPlayedModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        public AllMatchesPlayedModel(IMatchServices matchServices)
        {
            _matchServices = matchServices;
        }

        [BindProperty]
        public ListPaginated<Match> Matches { get; set; }
        [BindProperty]
        public ModelPagination Pagination { get; set; }
        public async Task<IActionResult> OnGetAsync(int side = 1, int size = 10)
        {
            Pagination = new ModelPagination(side, size);

            var matches = await _matchServices.GetAllAsync(Pagination);
            Matches = matches.Data;
            return Page();
        }
    }
}
