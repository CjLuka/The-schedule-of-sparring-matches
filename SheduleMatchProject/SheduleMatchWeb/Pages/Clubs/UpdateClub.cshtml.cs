using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Authorization;
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
        public UpdateClubModel(IClubServices clubServices, IGameClassServices gameClassServices, IUserServices userServices)
        {
            _clubServices = clubServices;
            _gameClassServices = gameClassServices;
            _userServices = userServices;
        }

        [BindProperty]
        public Club ClubUpdate { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }
        public List<SelectListItem> Users { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            List <SelectListItem> Users = new List<SelectListItem>();//utworzenie selectlisty dla wszystkich uzytkownikow
            var AllUsers = await _userServices.GetAllUsersAsync();//pobranie wszystkich uzytkownikow, aby wyswietlic ich podczas edycji jako admin
            var AllUsers2 = AllUsers.Data;//przypisanie danych do zmiennej
            foreach (var user in AllUsers2) 
            {
                Users.Add(new SelectListItem { Text = user.Email, Value = user.Id.ToString() });
            }
            ViewData["Users"] = Users;//przypisanie uzytkownikow do ViewData
            var response = await _clubServices.GetDetailClubAsync(id);
            ClubUpdate = response.Data;
            List<SelectListItem> GameClassess = new List<SelectListItem>();
            var AllGameClassess = await _gameClassServices.GetAllAsync();//Pobranie wszystkich klas rozgrywkowych
            foreach (var item in AllGameClassess)
            {
                GameClassess.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });//przypisanie wszystkich klas rozgrywkowych do selectlisty
            }
            ViewData["klasyRozgrywkowe"] = GameClassess;//przypisanie klas rozgrywkowych do ViewData
            if (User.Identity.IsAuthenticated)
            {
                string userEmail1 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//pobranie emailu zalogowanego uzytkownika
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
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
    }
}
