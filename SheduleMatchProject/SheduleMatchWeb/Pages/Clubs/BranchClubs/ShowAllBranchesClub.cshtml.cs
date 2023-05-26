using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistance.Repo.Interfaces;

namespace SheduleMatchWeb.Pages.Clubs.BranchClubs
{
    public class ShowAllBranchesClubModel : PageModel
    {
        private readonly IBranchClubRepository _branchClubRepository;
        public ShowAllBranchesClubModel(IBranchClubRepository branchClubRepository)
        {
            _branchClubRepository = branchClubRepository;
        }
        [BindProperty]
        public List<BranchClub> BranchClubss { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var allBranches = await _branchClubRepository.GetAllByClubAsync(id);
            if (allBranches != null && allBranches.Any())
            {
                BranchClubss = allBranches.ToList();
            }
            else
            {
                return NotFound();
            }
            return Page();
        }
    }
}
