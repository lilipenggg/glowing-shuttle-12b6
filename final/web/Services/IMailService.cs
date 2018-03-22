namespace web.Services
{
    public interface IMailService
    {
        void SendMail(string to, string subject, string body);
    }
}