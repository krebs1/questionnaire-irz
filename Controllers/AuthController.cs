using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using questionnaire.DTO;
using questionnaire.Services;

namespace questionnaire.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _config;

        public AuthController(IAuthService authService, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _authService = authService;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDTO user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successfuly done");
            }
            return BadRequest("Something went worng");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var checkUser = await _userManager.FindByEmailAsync(user.UserName);
            if (checkUser is null)
                return BadRequest($"Wrong username");

            if (! await _userManager.CheckPasswordAsync(checkUser, user.Password))
                return BadRequest($"Wrong password");

            var roles = await _userManager.GetRolesAsync(checkUser);

            var _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID",checkUser.Id.ToString()),
                    new Claim(_options.ClaimsIdentity.RoleClaimType,roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value)), 
                    SecurityAlgorithms.HmacSha512Signature
                    )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);
            return Ok(new { token });
        }
    }
