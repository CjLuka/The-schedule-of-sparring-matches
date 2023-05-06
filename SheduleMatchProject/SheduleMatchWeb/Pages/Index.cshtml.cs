using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistance.Repo.Interfaces;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace SheduleMatchWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IClubRepository _clubRepository;

        public IndexModel(ILogger<IndexModel> logger, IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;
        }

        public void OnGet()
        {
            var test = _clubRepository.GetAllAsync();
        }
    }
}