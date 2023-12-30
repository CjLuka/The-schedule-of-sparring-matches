using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages
{
    public class AllBranchesModel : PageModel
    {
        private readonly IBranchClubServices _branchClubServices;
        public AllBranchesModel(IBranchClubServices branchClubServices)
        {
            _branchClubServices = branchClubServices;
        }

        [BindProperty]
        public List<BranchClub> Branches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var allBranches = await _branchClubServices.GetAllBranchClubsAsync();

            Branches = allBranches.Data.ToList();
            return Page();
        }
    }
}
