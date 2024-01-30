using Aplication.Services.Interfaces;
using Application.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScheduleMatchWeb.Pages.Clubs.Admin
{
    public class AddNewFootballPitchModel : PageModel
    {
        private readonly IFootballPitchServices _footballPitchServices;
        private readonly IAdressServices _adressServices;

        public AddNewFootballPitchModel(IFootballPitchServices footballPitchServices, IAdressServices adressServices)
        {
            _adressServices = adressServices;
            _footballPitchServices = footballPitchServices;
        }
        [BindProperty]
        public FootballPitch FootballPitch { get; set; }

        public List<SelectListItem> Addresses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            List<SelectListItem> Addresses  = new List<SelectListItem>();
            var adressess = await _adressServices.GetAllAsync();
            foreach (var item in adressess.Data)
            {
                Addresses.Add(new SelectListItem { Text = item.City + item.Street, Value = item.Id.ToString() });
            }
            ViewData["adresy"] = Addresses;
            return Page();
        }
        public async Task <IActionResult> OnPostAsync()
        {
            await _footballPitchServices.AddAsync(FootballPitch);
            return RedirectToPage("/AllPitches");
        }
    }
}
