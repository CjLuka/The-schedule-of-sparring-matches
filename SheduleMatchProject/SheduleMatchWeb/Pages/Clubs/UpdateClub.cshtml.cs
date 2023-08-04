using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace SheduleMatchWeb.Pages.Clubs
{
    [Authorize(Roles = "Admin,President")]
    public class UpdateClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IGameClassServices _gameClassServices;
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        public UpdateClubModel(IClubServices clubServices, IGameClassServices gameClassServices, IUserServices userServices, UserManager<User> userManager)
        {
            _clubServices = clubServices;
            _gameClassServices = gameClassServices;
            _userServices = userServices;
            _userManager = userManager;
        }

        [BindProperty]
        public Club ClubUpdate { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }//Lista do wyœwietlania wszystkich lig
        public List<SelectListItem> Users { get; set; }//Lista do wyœwietlania u¿ytkowników
        public static string previousUserId;//Id prezesa który by³ ustawiony wczeœniej
        public async Task<IActionResult> OnGetAsync(int id)
        {
            List <SelectListItem> Users = new List<SelectListItem>();//utworzenie selectlisty dla wszystkich uzytkownikow

            var response = await _clubServices.GetDetailClubAsync(id);//pobranie danych na temat klubu po id
            ClubUpdate = response.Data;//przypisanie danych, aby wyswietlaly sie po wejsciu w edycje


            var UsersWithoutClub = await _userServices.GetPresidentWithoutClub();//pobranie uzytkownikow, ktorzy nie sa prezesami zadnego klubu
            foreach (var user in UsersWithoutClub.Data) 
            {
                Users.Add(new SelectListItem { Text = user.Email, Value = user.Id.ToString() });//dodanie uzytkownikow, ktorzy nie sa prezesami zadnego klubu do selectlisty
            }

            var currentlyPresident = await _userServices.GetUserById(ClubUpdate.UserId);
            previousUserId = currentlyPresident.Data.Id;

            var AllUsers = await _userServices.GetAllUsersAsync();//pobranie wszystkich uzytkownikow
            foreach (var user in AllUsers.Data)
            {
                if(user.Id == ClubUpdate.UserId)//Dopisanie uzytkownika ktory obecnie jest prezesem klubu
                {
                    Users.Add(new SelectListItem { Text = user.Email, Value = user.Id.ToString() });
                    //var removeRolesResult = await _userManager.RemoveFromRoleAsync(user2.Data, "President");
                }
                continue;
            }
            ViewData["Users"] = Users;//przypisanie listy uzytkownikow do ViewData
            



            List<SelectListItem> GameClassess = new List<SelectListItem>();
            var AllGameClassess = await _gameClassServices.GetAllAsync();//Pobranie wszystkich klas rozgrywkowych
            foreach (var item in AllGameClassess)
            {
                GameClassess.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich klas rozgrywkowych do selectlisty
            }
            ViewData["klasyRozgrywkowe"] = GameClassess;//przypisanie klas rozgrywkowych do ViewData


            if (User.Identity.IsAuthenticated)//jesli uzytkownik zalogowany
            {
                string userEmail1 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
            }
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateAsync(int id)//update klubu
        {
            if (ClubUpdate.Name.IsNullOrEmpty())
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Uzupe³nij wszystkie wymagane pola!"

                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Uzupe³nij wszystkie wymagane pola!";

                return Page();
            }

            try
            {
                string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
                ClubUpdate.LastModifiedBy = userEmail;
                
                await _clubServices.UpdateClubAsync(ClubUpdate, id, userEmail);
                ViewData["Notification"] = new Notification
                {
                    Message = "Rekord poprawnie edytowany",
                    Type = Domain.Models.Enum.NotificationType.Success
                };
                if (previousUserId != ClubUpdate.UserId)//w przypadku zmiany prezesa - usuniêcie roli staremu u¿ytkownikowi(rola president)
                {
                    //W przypadku zmiany, pobranie obu u¿ytkowników i przypisanie im odpowiednich ról
                    var oldPresident = await _userServices.GetUserById(previousUserId);
                    var newPresident = await _userServices.GetUserById(ClubUpdate.UserId);

                    await _userManager.AddToRoleAsync(newPresident.Data, "President");
                    await _userManager.RemoveFromRoleAsync(oldPresident.Data, "President");
                }
                return RedirectToPage("/Clubs/ShowAllClubs");
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Coœ posz³o nie tak",
                    Type = Domain.Models.Enum.NotificationType.Error
                };
            }

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)//usuwanie klubu
        {
            var deleted = await _clubServices.DeleteClubAsync(id);
            if (deleted.Success)
            {
                var oldPresident = await _userServices.GetUserById(previousUserId);//pobranie u¿ytkownika który by³ prezesem klubu
                await _userManager.RemoveFromRoleAsync(oldPresident.Data, "President");//usuniêcie roli prezesa przy usuniêciu klubu
                return RedirectToPage("./ShowAllClubs");
            }
            return Page();
        }
    }
}
