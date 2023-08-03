using Aplication.Services.Interfaces;
using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public Register RegisterViewModel { get; set; }
        public RegisterModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = new User
            {
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
                UserName = RegisterViewModel.Username,
                Email = RegisterViewModel.Email,
                Role = "User",
                NormalizedEmail = RegisterViewModel.Email.ToUpper()
            };
            var identityResult = await userManager.CreateAsync(user, RegisterViewModel.Password);
            try
            {
                if (identityResult.Succeeded)
                {
                    var addRolesResult = await userManager.AddToRoleAsync(user, "User");
                    if (addRolesResult.Succeeded)
                    {
                        ViewData["Notification"] = new Notification
                        {
                            Type = Domain.Models.Enum.NotificationType.Success,
                            Message = "Poprawnie zarejestrowano użytkownika"
                        };
                        return Page();
                    }

                }
            }
            catch (Exception)
            {
                await userManager.DeleteAsync(user);//usunięcie uzytkownika w przypadku problemu z przypisaniem roli
                throw;
            }


            ViewData["Notification"] = new Notification
            {
                Type = Domain.Models.Enum.NotificationType.Error,
                Message = "Coś poszło nie tak.."
            };
            return Page();
        }
    }
}
