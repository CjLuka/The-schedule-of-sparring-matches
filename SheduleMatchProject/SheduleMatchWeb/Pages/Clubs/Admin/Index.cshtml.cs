using Aplication.Services.Interfaces;
using Application.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ScheduleMatchWeb.Pages.Clubs.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IAdressServices _adressServices;

        public IndexModel(IFootballPitchServices footballPitchServices, IAdressServices adressServices)
        {
            _adressServices = adressServices;
            _footballPitchServices = footballPitchServices;
        }
        [BindProperty]
        public FootballPitch FootballPitch { get; set; }

        public List<SelectListItem> Addresses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            List<SelectListItem> Addresses = new List<SelectListItem>();
            var adressess = await _adressServices.GetAllAsync();
            foreach (var item in adressess.Data)
            {
                Addresses.Add(new SelectListItem { Text = item.City + item.Street, Value = item.Id.ToString() });
            }
            ViewData["adresy"] = Addresses;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            FootballPitch.CreatedDate = DateTime.Now;
            FootballPitch.LastModifiedDate = DateTime.Now;
            FootballPitch.CreatedBy = userIdString;

            await _footballPitchServices.AddAsync(FootballPitch);
            return RedirectToPage("/AllPitches");
        }
    }
}
