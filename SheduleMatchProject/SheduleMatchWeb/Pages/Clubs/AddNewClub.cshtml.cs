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
        [BindProperty]
        public Club NewClub { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }
        
        public async Task <IActionResult> OnGetAsync()
        {
            //Potrzebne przy tworzeniu nowego klubu, aby móc wybieraæ klasê rozgrywkow¹ z listy
            List<SelectListItem> GameClassess = new List<SelectListItem>();//Utworzenie selectlisty
            var AllGameClassess = await _gameClassServices.GetAllAsync();//Pobranie wszystkich klas rozgrywkowych
            foreach (var item in AllGameClassess)
            {
                GameClassess.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich klas rozgrywkowych do selectlisty
            }
            ViewData["klasyRozgrywkowe"] = GameClassess;//przypisanie klas rozgrywkowych do ViewData
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //foreach (var item in GameClassess)
            //{
            //    GameClassess.Add(item);
            //}
            //var addNewClub = await _clubServices.AddClubAsync(NewClub);
            NewClub.CreatedDate = DateTime.Now;
            NewClub.CreatedBy = "LukaTesty";
            NewClub.LastModifiedBy = "LukaTesty";
            await _clubServices.AddClubAsync(NewClub);

            return RedirectToPage("../Clubs/ShowAllClubs");
        }
    }
}
