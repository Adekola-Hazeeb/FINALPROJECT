using FINALPROJECT.Domain.Models.RequestModel;

namespace FINALPROJECT.Services.Interfaces
{
    public interface IEmailSender
    {
        void SendEMail(EmailRequestModel mailRequest);

    }
}
