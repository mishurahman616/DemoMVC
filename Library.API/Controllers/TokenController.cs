using Library.Infrastructure.Securities;
using Library.Persistence.Features.Memberships;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenService _tokenService;
        public TokenController(IConfiguration configuration, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<TokenController> logger, 
            ITokenService tokenService)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email, string password)
        {
            if(email == null || password == null)
            {
                return BadRequest("Invalid Credential");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest("Invalid Credential");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if(result==null || !result.Succeeded)
            {
                return BadRequest("Invalid Credential");
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var token = await _tokenService.GetJwtToken(user.UserName!, claims);
            return Ok(token);
        }
    }
}
