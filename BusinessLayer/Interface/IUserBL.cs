using ModelLayer.DTO;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        Task<string> Register(UserRegisterDTO userDto);
        Task<string> Login(UserLoginDTO userDto);
    }
}
