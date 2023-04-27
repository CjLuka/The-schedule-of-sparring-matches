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

        public List<Club> Clubs;
        public async Task<IActionResult> OnGetAsync()
        {
            var allClubs = _clubServices.GetAllAsync();
            Clubs = allClubs.Result.Data.ToList();
            if (!Clubs.Any())
            {
                return NotFound();
            }
            return Page();
        }
    }
}
