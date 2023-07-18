using Library.Persistence.Features.Memberships;
using Library.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Users=_userManager.Users;
            
            if (User.Identity?.IsAuthenticated??false)
            {
                List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role).ToList();
                var roles = new List<string>();

                foreach (var role in roleClaims)
                {
                    roles.Add(role.Value);
                }
                ViewBag.Roles = roles;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}