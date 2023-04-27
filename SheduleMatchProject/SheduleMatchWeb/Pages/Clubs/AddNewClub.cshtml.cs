using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistance.Migrations;
using Persistance.Repo.Interfaces;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class AddNewClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IGameClassServices _gameClassServices;
        public AddNewClubModel(IClubServices clubServices, IGameClassServices gameClassServices)
        {
            _clubServices = clubServices;
            _gameClassServices = gameClassServices;
        }

        //private readonly IGameClassRepository _gameClassRepository;
        //public AddNewClubModel(IClubServices clubServices, IGameClassRepository gameClassRepository)
        //{
        //    _clubServices = clubServices;
        //    _gameClassRepository = gameClassRepository;
        //}
        [BindProperty]
        public Club NewClub { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }
        
        public async Task <IActionResult> OnGetAsync()
        {
            List<SelectListItem> GameClassess = new List<SelectListItem>();
            var AllGameClassess = await _gameClassServices.GetAllAsync();
            foreach (var item in AllGameClassess)
            {
                GameClassess.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewData["klasyRozgrywkowe"] = GameClassess;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var item in GameClassess)
            {
                GameClassess.Add(item);
            }
            //var addNewClub = await _clubServices.AddClubAsync(NewClub);
            NewClub.CreatedDate = DateTime.Now;
            NewClub.CreatedBy = "LukaTesty";
            NewClub.LastModifiedBy = "LukaTesty";
            //NewClub.GameClass = 1;
            await _clubServices.AddClubAsync(NewClub);

            return RedirectToPage("../Index");
        }
    }
}
