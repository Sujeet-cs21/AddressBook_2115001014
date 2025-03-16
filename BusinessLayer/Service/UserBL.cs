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

        public UserBL(IUserRL userRL, TokenService tokenService)
        {
            _userRL = userRL;
            _tokenService = tokenService;
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
    }
}