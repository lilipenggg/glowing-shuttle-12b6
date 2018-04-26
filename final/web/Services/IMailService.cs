using web.Models;

namespace web.Services
{
    public interface IMailService
    {
        void SendMail(EmailModel emailModel);
    }
}