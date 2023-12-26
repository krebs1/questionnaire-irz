using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using questionnaire.Contracts;
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

        public AuthController(IAuthService authService, UserManager<IdentityUser> userManager, IConfiguration config, SignInManager<IdentityUser> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDTO user)
        {
            try
            {
                var result = await _authService.RegisterUser(user);
                if(result.Succeeded)
                    return CreatedAtAction(nameof(RegisterUser), new {user.UserName});

                return BadRequest("Пользователь с этим именем уже существует");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Внутрення ошибка сервера");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                
                var identityUser = await _userManager.FindByEmailAsync(user.UserName);
                if (identityUser == null)
                    return BadRequest("Неверное имя пользователя");

                if (! await _userManager.CheckPasswordAsync(identityUser, user.Password))
                    return BadRequest("Неверный пароль");

                var token = await _authService.Login(identityUser);
                
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_config.GetSection("Jwt:ExpiresSeconds").Value)),
                    HttpOnly = true,
                });
            
                return Ok(new { token });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Внутрення ошибка сервера");
            }
        }
        
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Response.Cookies.Delete("token");
                
                return Ok("Выход произошел успешно");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Внутрення ошибка сервера");
            }
        }
    }
