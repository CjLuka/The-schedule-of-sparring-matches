using Domain.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SheduleMatchWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public User NewUser { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task <IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task <IActionResult> OnPostAsync()
        {

        }
    }
}
