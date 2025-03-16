using ModelLayer.DTO;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        Task<string> Register(UserRegisterDTO userDto);
        Task<string> Login(UserLoginDTO userDto);
        Task<string> ForgotPassword(ForgotPasswordDTO forgotPasswordDto);
        Task<string> ResetPassword(ResetPasswordDTO resetPasswordDto);
    }
}
