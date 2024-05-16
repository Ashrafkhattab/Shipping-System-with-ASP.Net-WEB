using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shapping.Handler;
using Shipping.Core.Model;
using Shipping.Core.Repositries.contract;
using Shipping.DTO;
using Shipping.DTO.RegestarDto;
using Shipping.Services.Handler;


namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountUserController : Controller
    {
        private readonly AccountHandler  _accountHandler;
        private readonly IAccountUser _accountRepository;
        private readonly UserManager<AppUser> _userManager;

        public AccountUserController(UserManager<AppUser> userManager, IConfiguration config, IAccountUser accountRepository)
        {
            _accountHandler = new AccountHandler(userManager, config);
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDto registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = registerUser.UserName,
                    Name = registerUser.Name,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                    BranchId = registerUser.BranchId,
                    Address = registerUser.Address,
                };

                var result = await _accountRepository.CreateUser(user, registerUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role,  "Employee")

            };
                    await _userManager.AddClaimsAsync(user, claims);

                    return Ok(new { message = "Employee Add Success" });
                }
                else
                {
                    return BadRequest(string.Join(",", result.Errors.Select(e => e.Description).ToList()));
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            var result = await _accountRepository.GetAllEmployees();
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUser(loginUser.UserName);
                if (user != null)
                {
                    var found = await _accountRepository.GetPassword(user, loginUser.Password);
                    if (found)
                    {
                        var (token, expiration) = await _accountHandler.GenerateJwtTokenAsync(user);
                        return Ok(new
                        {
                            token,
                            expiration,
                        });
                    }
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
