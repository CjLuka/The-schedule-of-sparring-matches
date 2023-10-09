using Aplication.Services.Interfaces;
using Domain.Models.Domain;
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
        public List<Club> Clubs { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var allClubs = await _clubServices.GetAllAsync();
            Clubs = allClubs.Data;
            //if (!Clubs.Any())
            //{
            //    return NotFound();
            //}
            return Page();
        }
    }
}
