using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class UpdateClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IGameClassServices _gameClassServices;
        public UpdateClubModel(IClubServices clubServices, IGameClassServices gameClassServices)
        {
            _clubServices = clubServices;
            _gameClassServices = gameClassServices;
        }

        [BindProperty]
        public Club ClubUpdate { get; set; }
        [BindProperty]
        public List<SelectListItem> GameClassess { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
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
