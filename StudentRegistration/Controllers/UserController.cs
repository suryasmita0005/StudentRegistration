using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using StudentRegistration.Repositories;

namespace StudentRegistration.Controllers
{
    
    public class UserController : Controller
    {
        private B2CUsersService _graphAPIService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        public UserController(IConfiguration configuration,
        ILogger<UserController> logger, B2CUsersService graphAPIService)
        {
            _configuration = configuration;
            _logger = logger;
            _graphAPIService = graphAPIService;
        }

        public bool IsAdmin()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return false;
            }

            var claimsPrincipal = User;
            var claims = claimsPrincipal.Claims.ToList();

            var roleClaim = claims.FirstOrDefault(c => c.Type == "extension_Role");

            return roleClaim != null && roleClaim.Value == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            var isAdmin = IsAdmin(); 

            if (isAdmin)
            {
                var users = await _graphAPIService.GetUsersAsync();
                return View(users);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

    }
}

