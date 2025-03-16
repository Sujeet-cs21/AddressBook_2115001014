using BusinessLayer.Hashing;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        private readonly TokenService _tokenService;
        private readonly Password_Hash _passwordHash = new Password_Hash();
        private readonly IEmailService _emailService;

        public UserBL(IUserRL userRL, TokenService tokenService, IEmailService emailService)
        {
            _userRL = userRL;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<string> Register(UserRegisterDTO userDto)
        {
            var existingUser = await _userRL.GetUserByEmail(userDto.Email);
            if (existingUser != null)
                throw new System.Exception("User already exists");

            var user = new UserEntity
            {
                Email = userDto.Email,
                Password = _passwordHash.HashPassword(userDto.Password)
            };

            await _userRL.CreateUser(user);
            return _tokenService.GenerateToken(user.Email);
        }

        public async Task<string> Login(UserLoginDTO userDto)
        {
            var user = await _userRL.GetUserByEmail(userDto.Email);
            if (user == null || _passwordHash.VerifyPassword(userDto.Password, user.Password))
                throw new System.Exception("Invalid credentials");

            return _tokenService.GenerateToken(user.Email);
        }
        private DateTime ResetTokenExpiry;
        public async Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDto)
        {
            var user = await _userRL.GetUserByEmail(forgotPasswordDto.Email);
            if (user == null)
                throw new Exception("User not found");

             var ResetToken = Guid.NewGuid().ToString();
             ResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            await _userRL.UpdatePassword(user);
            _emailService.SendPasswordResetEmail(user.Email, ResetToken);

            return "Password reset email sent.";
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            var user = await _userRL.GetUserByEmail(resetPasswordDto.Token);
            if (user == null || ResetTokenExpiry < DateTime.UtcNow)
                throw new Exception("Invalid or expired token");

            user.Password = _passwordHash.HashPassword(resetPasswordDto.NewPassword);
            ResetTokenExpiry = DateTime.MinValue;

            await _userRL.UpdatePassword(user);
            return "Password reset successfully.";
        }
    }
}