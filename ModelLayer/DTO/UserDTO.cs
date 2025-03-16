namespace ModelLayer.DTO
{
    public class UserRegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDTO
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}
