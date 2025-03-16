namespace BusinessLayer.Interface
{
    public interface IEmailService
    {
        void SendPasswordResetEmail(string toEmail, string resetToken);
    }
}
