using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistance.Migrations;
using Persistance.Repo.Interfaces;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs
{
    [Authorize(Roles = "Admin")]
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
            NewClub.CreatedDate = DateTime.Now;
            NewClub.CreatedBy = "LukaTesty";
            string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
            NewClub.LastModifiedBy = userEmail;
            NewClub.UserId = 1002;
            await _clubServices.AddClubAsync(NewClub);

            return RedirectToPage("../Clubs/ShowAllClubs");
        }
    }
}
