using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.Emails.Core;
using Serendip.IK.Emails.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Emails
{
    public interface IEmailAppService : IApplicationService
    {
         
        Task<EmailDto> Send(EmailDto email);

    }
}