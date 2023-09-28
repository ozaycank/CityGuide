using CityGuide.Data;
using CityGuide.Dtos;
using CityGuide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository AuthRepository;
        IConfiguration _configuration;

        public AuthController(IConfiguration configuration,IAuthRepository authRepository)
        {
            _configuration = configuration;
            AuthRepository = authRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto user, UserForRegisterDto userForRegisterDto, UserForRegisterDto userForRegisterDto)
        {
            if (await AuthRepository.UserExists(userForRegisterDto.UserName)) {
                ModelState.AddModelError("UserName", "UserName already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };
            var createdUser = await AuthRepository.Register(userToCreate,userForRegisterDto.Password);
            return StatusCode(201, createdUser.UserName);
        }
    }
}
