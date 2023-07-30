using Domain.Models.Domain;
using Domain.Models.VievModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace SheduleMatchWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        [BindProperty]
        public Login LoginViewModel { get; set; }
        public LoginModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(LoginViewModel.Username, LoginViewModel.Password, false, false);
            if(signInResult.Succeeded)
            {
                if(!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToPage(returnUrl);
                }
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Notification"] = new Notification
                {
                    Type = Domain.Models.Enum.NotificationType.Error,
                    Message = "B³¹d podczas próby logowania"
                };
                return Page();
            }
            
        }
    }
}
