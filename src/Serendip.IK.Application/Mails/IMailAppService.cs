using Abp.Application.Services;
using Serendip.IK.Mails.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Mails
{
    public interface IMailAppService : IAsyncCrudAppService<MailDto, long>
    {
        bool SendMail(string from, string to, string cc, string subject, string body, string bcc);

        Task<bool> SendWithExtension(SendMailDto mail);
    }
}
