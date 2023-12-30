using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace SheduleMatchWeb.Pages.Clubs
{
    public class ShowAllClubsModel : PageModel
    {
        private readonly IClubServices _clubServices;

        public ShowAllClubsModel(IClubServices clubServices)
        {
            _clubServices = clubServices;
        }

        [BindProperty]
        public ListPaginated<Club> Clubs { get; set; }


        public async Task<IActionResult> OnGetAsync(int page = 1, int size = 1)
        {
   
            var allClubs = await _clubServices.GetAllAsync(new ModelPagination(page, size));
            Clubs = allClubs.Data;

            return Page();
        }

    }
}
