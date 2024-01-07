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
        [BindProperty]
        public ModelPagination Pagination { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int side =1, int size = 10)
        {
            Pagination = new ModelPagination(side, size);
   
            var allClubs = await _clubServices.GetAllAsync(Pagination);
            Clubs = allClubs.Data;

            return Page();
        }

    }
}
