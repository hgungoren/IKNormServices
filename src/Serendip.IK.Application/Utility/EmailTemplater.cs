using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Services;

namespace Serendip.IK.Utility
{
    public class EmailTemplater: IDomainService,ITransientDependency
    {
        private ISettingManager _settingManager;

        public EmailTemplater(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }
        public string ProcessTemplate(int tenantId,string templateName, EmailNotificationModel model)
        {
            var template = _settingManager.GetSettingValueForTenant($"mail.template.{templateName}", tenantId);

            template = template.Replace("@Model.Message", model.Message).Replace("@Model.UnsubscribeUrl", model.UnsubscribeUrl).Replace("@Model.ViewDetailUrl", model.ViewDetailUrl);

            return template;
        }
    }
}
