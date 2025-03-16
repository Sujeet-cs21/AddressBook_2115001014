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

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDto)
        {
            var result = await _userBL.ForgotPassword(forgotPasswordDto);
            return Ok(new { Message = result });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            var result = await _userBL.ResetPassword(resetPasswordDto);
            return Ok(new { Message = result });
        }
    }
}
