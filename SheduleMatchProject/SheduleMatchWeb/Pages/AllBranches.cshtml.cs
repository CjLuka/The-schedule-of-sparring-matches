using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
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
        public ListPaginated<BranchClub> Branches { get; set; }
        [BindProperty]
        public ModelPagination Pagination { get; set; }

        public async Task<IActionResult> OnGetAsync(int side = 1, int size = 10)
        {
            Pagination = new ModelPagination(side, size);
            var allBranches = await _branchClubServices.GetAllBranchClubsAsync(Pagination);

            Branches = allBranches.Data;
            return Page();
        }
    }
}
