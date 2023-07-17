using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Persistance.Migrations;
using Persistance.Repo.Interfaces;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs
{
    [Authorize(Roles = "Admin")]
    public class AddNewClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IGameClassServices _gameClassServices;
        private readonly IUserServices _userServices;
        public AddNewClubModel(IClubServices clubServices, IGameClassServices gameClassServices, IUserServices userServices)
        {
            _clubServices = clubServices;
            _gameClassServices = gameClassServices;
            _userServices = userServices;
        }
        [BindProperty]
        public newClub NewClub { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }
        public List<SelectListItem> Users { get; set; }
        
        public async Task <IActionResult> OnGetAsync()
        {
            //Potrzebne przy tworzeniu nowego klubu, aby m�c wybiera� klas� rozgrywkow� z listy
            List<SelectListItem> GameClassess = new List<SelectListItem>();//Utworzenie selectlisty dla klas rozgrywkowych
            List<SelectListItem> Users = new List<SelectListItem>();//Utworzenie selectlisty dla uzytkownikow

            var AllGameClassess = await _gameClassServices.GetAllAsync();//Pobranie wszystkich klas rozgrywkowych
            foreach (var item in AllGameClassess)
            {
                GameClassess.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich klas rozgrywkowych do selectlisty
            }
            ViewData["klasyRozgrywkowe"] = GameClassess;//przypisanie klas rozgrywkowych do ViewData


            var UsersWithoutClub = await _userServices.GetPresidentWithoutClub();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
            foreach (var user in UsersWithoutClub.Data)
            {
                Users.Add(new SelectListItem { Text = user.Email, Value = user.Id.ToString() });//dodanie uzytkownikow, ktorzy nie sa prezesami zadnego klubu do selectlisty
            }
            ViewData["Users"] = Users;//przypisanie listy uzytkownikow do ViewData
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
                NewClub.CreatedDate = DateTime.Now;//Data utworzenia - obecna
                NewClub.FeaturedImageUrl = "test";
                NewClub.LastModifiedBy = userEmail;//ostatnia modyfikacja przez uzytkownika zalogowanego
                NewClub.CreatedBy = userEmail;//utworzenie przez uzytkownika zalogowanego

                await _clubServices.AddClubAsync(NewClub);

                return RedirectToPage("../ShowAllClubs");
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Co� posz�o nie tak",
                    Type = Domain.Models.Enum.NotificationType.Error
                };
            }
            return Page();
            
        }
    }
}
