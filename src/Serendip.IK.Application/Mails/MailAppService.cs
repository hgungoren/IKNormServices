using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Extensions;
using Serendip.IK.Helpers;
using Serendip.IK.Mails.Dto;
using System;
using System.Threading.Tasks;

namespace Serendip.IK.Mails
{
    public class MailAppService : AsyncCrudAppService<Mail, MailDto, long>, IMailAppService
    {
        private IRepository<Mail, long> _repository;
        private IExtensionManager _extensionManager;

        public MailAppService(IRepository<Mail, long> repository, IExtensionManager extensionManager) : base(repository)
        {
            _repository = repository;
            _extensionManager = extensionManager;
        }
        public bool SendMail(string from, string to, string cc, string subject, string body, string bcc)
        {
            MailDto input = new MailDto();

            input.To = to;
            input.Subject = subject;
            input.Body = body;
            input.Cc = cc;
            input.Bcc = bcc;
            input.From = from;

            input.IsSend = false;

            MailHelper mailHelper = new MailHelper();
            var resSend = mailHelper.SendMail(input.From, input.To, input.Subject, input.Body, input.Bcc);

            if (resSend)
            {
                input.IsSend = true;
                input.SendDate = DateTime.UtcNow;
                Repository.Insert(MapToEntity(input));
                return true;
            }
            else
            {
                Repository.Insert(MapToEntity(input));
            }

            return false;
        }

        public async Task<bool> SendWithExtension(SendMailDto mail)
        {
            var result = await _extensionManager.Invoke(ExtensionTypes.MAIL, mail);
            return result.Success;
        }
    }
}
