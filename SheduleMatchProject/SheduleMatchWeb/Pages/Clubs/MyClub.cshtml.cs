using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class MyClubModel : PageModel
    {
        private readonly IClubServices _clubServices;
        private readonly IUserServices _userServices;

        public MyClubModel(IClubServices clubServices, IUserServices userServices)
        {
            _clubServices = clubServices;
            _userServices = userServices;
        }
        [BindProperty]
        public Club myClub { get; set; }
        public async Task <IActionResult> OnGetAsync()
        {
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby wyswietlic odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int

            ClaimsPrincipal currentUser = this.User;//pobranie u¿ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);

            var myClub2 = await _clubServices.GetClubByPresidentIdAsync(userIdString);//wyswietlenie klubu zalogowanego uzytkownika

            myClub = myClub2.Data;//przypisanie danych do bindProperty
            return Page();
        }
    }
}
