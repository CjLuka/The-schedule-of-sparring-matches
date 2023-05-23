using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

namespace SheduleMatchWeb.Pages.Account
{

    public class RegisterModel : PageModel
    {
        private readonly IUserServices _userServices;
        public RegisterModel(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [BindProperty]
        public User newUser { get; set; }
        public async Task <IActionResult> OnGetAsync()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Index");
            }
            return Page();
        }

        public async Task <IActionResult> OnPostAsync()
        {
            if (newUser.FirstName.IsNullOrEmpty()|| newUser.LastName.IsNullOrEmpty() 
                || newUser.Email.IsNullOrEmpty()|| newUser.Password.IsNullOrEmpty())
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Uzupe³nij wszystkie pola!"

                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Uzupe³nij wszystkie pola!";
                return Page();
            }
            var checkEmail = await _userServices.GetEmailAsync(newUser.Email);
            
            if (checkEmail == null)
            {
                newUser.Role = "User";
                await _userServices.AddAsync(newUser);
                return RedirectToPage("/Index");
            }
            if (checkEmail != null)
            {
                var notification = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "Podany adres email ju¿ istnieje w bazie danych."
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                ViewData["MessageValidation"] = "Podany adres email ju¿ istnieje w bazie danych.";
                return Page();
            }
            return Page();
        }
    }
}
