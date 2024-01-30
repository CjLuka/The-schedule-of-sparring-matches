using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleMatchWeb.Pages.Matches
{
    public class AllMatchesPlannedModel : PageModel
    {
        private readonly IMatchRequestServices _matchRequestServices;
        public AllMatchesPlannedModel(IMatchRequestServices matchRequestServices)
        {
            _matchRequestServices = matchRequestServices;
        }

        [BindProperty]
        public ListPaginated<MatchRequest> MatchRequests { get; set; }
        [BindProperty]
        public ModelPagination Pagination { get; set; }
        public async Task<IActionResult> OnGetAsync(int side = 1, int size = 10)
        {
            Pagination = new ModelPagination(side, size);
            var matchRequests = await _matchRequestServices.GetAllMatchRequestsAsync(Pagination);

            MatchRequests = matchRequests.Data;
            return Page();
        }
    }
}
