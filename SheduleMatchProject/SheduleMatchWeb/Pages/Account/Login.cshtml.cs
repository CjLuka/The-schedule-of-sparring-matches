using Domain.Models.Domain;
using Domain.Models.VievModel;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Aplication.Services.Interfaces;
using Persistance.Repo.Interfaces;

namespace SheduleMatchWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserServices _userServices;
        public LoginModel(IUserServices userServices)
        {
            _userServices = userServices;
        }
        //private readonly IUserRepository _userRepository;
        //public LoginModel(IUserRepository userRepository)
        //{
        //    _userRepository= userRepository;
        //}

        [BindProperty]
        public User User { get; set; }
        public async Task <IActionResult> OnGetAsync()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Index");
            }
            return Page();
        }
        public async Task <IActionResult> OnPostAsync(User userLogin)
        {
            if (User.Email.IsNullOrEmpty() || User.Password.IsNullOrEmpty())
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Uzupe�nij wszystkie wymagane pola!"

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Uzupe�nij wszystkie wymagane pola!";
                return Page();
            }
            var emailFromBase = await _userServices.GetEmailAsync(User.Email);//pobranie emaila
            var passwordFromBase = await _userServices.GetPasswordByEmailAsync(User.Email);//pobranie has�a
            if (emailFromBase == null)//funkcja zwracaj�ca email jest ustawiona na zwracanie nulla, jesli nie znajdzie maila w bazie
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Podany adres email nie istnieje w bazie danych"

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Podany adres email nie istnieje w bazie danych";
                return Page();
            }
            if (passwordFromBase.ToString() != User.Password)//sprawdzenie czy haslo jest takie samo jak wprowadzone przez uzytkownika
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "B��dne has�o!"

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);//JsonSerializer pomaga przenie�� dane, poniewa� typ notification nie jest zwyklym prostym typem
                ViewData["MessageValidation"] = "B��dne has�o!";
                
                return Page();
                
            }
            var rola = await _userServices.GetRoleByEmailAsync(User.Email);//pobranie roli
            var userId = await _userServices.GetUserIdByEmailAsync(User.Email);//pobranie userId
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, User.Email),//obiekt klasy Claim reprezentuj�cy podstawowe dane uwierzytelniaj�ce u�ytkownika, w tym wskazuj�cy na identyfikator u�ytkownika i jego adres e-mail 
                new Claim("OtherProperties", _userServices.GetRoleByEmailAsync(User.Email).ToString()),//niestandardowy obiekt klasy Claim, kt�ry przechowuje dodatkowe w�a�ciwo�ci w tym wypadku role uzytkownika
                new Claim("UserId", userId.ToString()),//
                new Claim(ClaimTypes.Role, rola)//obiekt claim ktory pobiera Id zalogowanego uzytkownika
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh= true//AllowRefresh oznacza, �e u�ytkownik mo�e od�wie�y� token uwierzytelniaj�cy, zanim wyga�nie
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);// Metoda u�ywana do zapisania tokenu uwierzytelniaj�cego dla zalogowanego u�ytkownika.
            return RedirectToPage("/Index");
        }
    }
}
