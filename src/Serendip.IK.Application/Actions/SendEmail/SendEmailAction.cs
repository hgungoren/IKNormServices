using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Newtonsoft.Json;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Emails;
using Serendip.IK.Emails.Dto;
using Serendip.IK.KNorms;
using Serendip.IK.KNorms.Dto;
using System.Linq;

namespace Serendip.IK.Actions.SendEmail
{
    public class SendEmailAction : BaseAction, ISingletonDependency, IApplicationService
    {
        private readonly IEmailAppService emailAppService;
        private readonly IRepository<User, long> userRepository;
        private readonly IKNormAppService _kNormAppService;

        public SendEmailAction(IEmailAppService emailAppService, IRepository<User, long> userRepository, IKNormAppService kNormAppService)
        {
            this.emailAppService = emailAppService;
            this.userRepository = userRepository;
            this._kNormAppService = kNormAppService;
        }

        public override string Name => "send-email";

        [UnitOfWork]
        public override void Invoke(ActionContext context)
        {
            var data = JsonConvert.DeserializeObject<SendEmailData>(JsonConvert.SerializeObject(context.Data));

            var entity = context.EventParameter;

            var dto = new EmailDto();
            dto.Subject = data.Subject;
            dto.Body = data.Body;
            dto.TemplateId = data.TemplateId;
            dto.ModelName = entity.ModelName;
            dto.ProviderAccountId = data.ProviderAccountId;
            dto.EmailAttachments = data.EmailAttachments;
            dto.EmailRecipients = data.EmailRecipients;

            dto.ModelId = entity.Id;
            dto.CreatorUserId = context.CurrentUserId;
            dto.Date = context.EventParameter.Date;

            MailActionTemplateModel mailData = new MailActionTemplateModel();

            if (data.SendToOwner)
            {
                var user = userRepository.FirstOrDefault(x => x.Id == context.EventParameter.UserId.Value);
                dto.EmailRecipients.Add(new EmailRecipientDto
                {
                    EmailAddress = user.EmailAddress,
                    Type = RecipientType.CC,
                    AddAsNew = true,
                    ModelId = user.Id.ToString(),
                    ModelName = user.FullName
                });
            }

            if (!data.SendToCustomer && data.EmailRecipients.Count() > 0)
            {
                emailAppService.Send(dto).Wait();
            }

            if (data.SendToCustomer)
            {
                if (entity.ModelName == ModelTypes.LEAD || entity.ModelName == ModelTypes.ACCOUNT || entity.ModelName == ModelTypes.CONTACT)
                {
                    if (entity.ModelName == ModelTypes.KNORM)
                    {
                        KNormDto entityDto = _kNormAppService.GetAsync(new EntityDto<long> { Id = dto.ModelId }).Result;
                        dto.EmailRecipients.Add(new EmailRecipientDto
                        {
                            EmailAddress = "murat.vuranok@suratkargo.com.tr",
                            Type = RecipientType.To,
                            AddAsNew = false,
                            ModelId = dto.ModelId.ToString(),
                            ModelName = entity.ModelName
                        });

                        emailAppService.Send(dto).Wait();
                    }

                }
            }

            //var emailSender = IocManager.Instance.Resolve<IEmailSender>();
            //emailSender.Send(data.To, data.Subject, data.Body);
        }
    }
}
