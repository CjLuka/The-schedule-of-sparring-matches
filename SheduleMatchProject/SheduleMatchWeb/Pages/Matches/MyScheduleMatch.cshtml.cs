using Aplication.Services.Interfaces;
using Aplication.Services.Services;
using Domain.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SheduleMatchWeb.Pages.Clubs
{
    public class MyScheduleMatchModel : PageModel
    {
        private readonly IMatchServices _matchServices;
        private readonly IClubServices _clubServices;
        public MyScheduleMatchModel(IMatchServices matchServices, IClubServices clubServices)
        {
            _matchServices = matchServices;
            _clubServices = clubServices;
        }
        [BindProperty]
        public List<Match> Matches { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ClaimsPrincipal currentUser = this.User;//pobranie u�ytkownika
            string userIdString = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Guid.TryParse(userIdString, out var userId);
            //string userIdString = HttpContext.User.FindFirstValue("UserId");//pobranie userId zalogowanego uzytkownika, aby m�c pobra� odpowiedni klub
            //int.TryParse(userIdString, out int userId);//przerobieine userId na int

            var Club = await _clubServices.GetClubByPresidentIdAsync(userIdString);//pobranie klubu przypisanego do zalogowanego uzytkownika
            try
            {
                var Matches2 = await _matchServices.GetAllByClubAsync(Club.Data.Id);
                Matches = Matches2.Data;//przypisanie danych do bindProperty
            }
            catch (Exception)
            {
                throw;
            }
            

            

            return Page();
        }
    }
}
