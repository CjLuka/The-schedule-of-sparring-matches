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
            var emailFromBase = await _userServices.GetEmailAsync(User.Email);
            var passwordFromBase = await _userServices.GetPasswordByEmailAsync(User.Email);
            if (emailFromBase.ToString() != User.Email)
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
            if (emailFromBase == null)
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
            if (passwordFromBase.ToString() != User.Password)
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "B��dne has�o!"

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "B��dne has�o!";
                return Page();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, User.Email),//obiekt klasy Claim reprezentuj�cy podstawowe dane uwierzytelniaj�ce u�ytkownika, w tym wskazuj�cy na identyfikator u�ytkownika i jego adres e-mail 
                new Claim("OtherProperties", _userServices.GetRoleByEmailAsync(User.Email).ToString())//niestandardowy obiekt klasy Claim, kt�ry przechowuje dodatkowe w�a�ciwo�ci w tym wypadku role uzytkownika
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
