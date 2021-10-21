using Abp.Application.Services;
using Serendip.IK.Emails.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Emails
{
    public interface IEmailAppService : IApplicationService
    {
         
        Task<EmailDto> Send(EmailDto email);

    }
}