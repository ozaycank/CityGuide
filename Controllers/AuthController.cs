using CityGuide.Data;
using CityGuide.Dtos;
using CityGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository AuthRepository;
        IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            AuthRepository = authRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userDto)
        {
            if (await AuthRepository.UserExists(userDto.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already exists");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                UserName = userDto.UserName
            };
            var createdUser = await AuthRepository.Register(userToCreate, userDto.Password);
            return StatusCode(201, createdUser.UserName);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await AuthRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            // Retrieve the JWT token key from configuration
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            // Generate JWT token using the configured options
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName)
    };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenString);
        }


    }

}
