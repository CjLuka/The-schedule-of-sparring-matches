using Domain.Models.VievModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public Register RegisterViewModel { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.Username,
                Email = RegisterViewModel.Email,
            };

            var identityResult = await userManager.CreateAsync(user, RegisterViewModel.Password);

            if (identityResult.Succeeded)
            {
                var addRolesResult = await userManager.AddToRoleAsync(user, "User");
                if(addRolesResult.Succeeded)
                {
                    ViewData["Notification"] = new Notification
                    {
                        Type = Domain.Models.Enum.NotificationType.Success,
                        Message = "Poprawnie zarejestrowano u¿ytkownika"
                    };
                    return Page();
                }
                
            }
            ViewData["Notification"] = new Notification
            {
                Type = Domain.Models.Enum.NotificationType.Error,
                Message = "Coœ posz³o nie tak.."
            };
            return Page();
        }
    }
}
