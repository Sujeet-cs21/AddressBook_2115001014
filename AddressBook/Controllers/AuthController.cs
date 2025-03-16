using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;

namespace AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public AuthController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserRegisterDTO userDto)
        {
            var token = await _userBL.Register(userDto);
            return Ok(new { Message = $"Successfully Registered with Email : {userDto.Email}" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO userDto)
        {
            var token = await _userBL.Login(userDto);
            return Ok(new { Message = "Login Successful",Token = token });
        }
    }
}
