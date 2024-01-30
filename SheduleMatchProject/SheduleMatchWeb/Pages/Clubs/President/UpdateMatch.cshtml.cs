using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScheduleMatchWeb.Pages.Clubs.President
{
    [Authorize(Roles = "President")]
    public class UpdateMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        public UpdateMatchModel(IMatchServices matchServices)
        {
            _matchServices = matchServices;
        }
        [BindProperty]
        public Match editMatch { get; set; }
        public async Task <IActionResult> OnGetAsync(int id)
        {
            var match = await _matchServices.GetByIdAsync(id);

            editMatch = match.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _matchServices.UpdateAsync(editMatch, id);
            return RedirectToPage("/Clubs/President/MatchesPlayed");
        }
    }
}
