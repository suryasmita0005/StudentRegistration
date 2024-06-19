using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using StudentRegistration.Repositories;
using StudentRegistration.ViewModel;

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

        //To check Admin or not
        public bool IsAdmin()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return false;
            }

            var claimsPrincipal = User;
            var claims = claimsPrincipal.Claims.ToList();

            var roleClaim = claims.FirstOrDefault(c => c.Type == "jobTitle");

            return roleClaim != null && roleClaim.Value == "Admin";
        }

        //GET: User/Index
        [HttpGet]
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

        //GET: User/Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // POST: User/Add
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _graphAPIService.CreateUserAsync(model);
                    return RedirectToAction("Index"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating user: {ex.Message}"); 
                }
            }

            return View(model);
        }

        //GET: User/Edit
        public async Task<IActionResult> Edit()
        {
            return View();
        }

    }
}

