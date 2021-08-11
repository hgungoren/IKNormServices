using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Notifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Notification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serendip.IK.Actions.SendNotification
{
    public class SendNotificationAction : BaseAction, ISingletonDependency, IApplicationService
    {
        private readonly INotificationPublisher notificationPublisher;
        private readonly IRepository<User, long> userRepository;
        private readonly ISuratNotificationService SuratNotificationService;

        public SendNotificationAction(INotificationPublisher notificationPublisher, IRepository<User, long> userRepository,
            ISuratNotificationService SuratNotificationService)
        {
            this.notificationPublisher = notificationPublisher;
            this.userRepository = userRepository;
            this.SuratNotificationService = SuratNotificationService;
        }


        public override string Name => "send-notification";

        [UnitOfWork]
        public async override void Invoke(ActionContext context)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<SendNotificationData>(JsonConvert.SerializeObject(context.Data), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
                 
                var notifData = new LocalizableMessageNotificationData(new LocalizableString(context.EventParameter.EventName, CoreConsts.LocalizationSourceName));
                notifData["detail"] = data.Description + " - " + context.EventParameter.Name;
                var url = "";// UrlGenerator.FullUrl($"{context.EventParameter.ModelName}_view", new { id = context.EventParameter.Id });

                User user = userRepository.FirstOrDefault(x => x.Id == context.EventParameter.UserId.Value);
                notifData["currentUser"] = user.FullName;
                notifData["url"] = context.EventParameter.Url;

                List<long> userIds = new List<long>();

                //foreach (var item in data.OwnerGroupId)
                //{
                //    userIds = userGroupRepository.GetAll().Where(x => x.UserGroupId == item).Select(s => s.UserId).ToList();
                //}

                if (data.OwnerIds != null)
                {
                    foreach (var item in data.OwnerIds)
                    {
                        if (!userIds.Contains(item))
                        {
                            userIds.Add(item);
                        }
                    }
                }

                if (data.isCurrentUser && !userIds.Contains(context.CurrentUserId.Value))
                {
                    userIds.Add(context.CurrentUserId.Value);
                }

                var userIdentifiers = new List<Abp.UserIdentifier>();
                foreach (var id in userIds)
                {
                    userIdentifiers.Add(new Abp.UserIdentifier(context.CurrentTenantId.Value, id));
                }
               
                var toUserIds = userIdentifiers.Select(s => s.UserId.ToString()).ToArray();
                if (toUserIds.Count() > 0)
                    SuratNotificationService.PrepareNotification(notifData, context.CurrentTenantId.Value, context.CurrentUserId.Value, toUserIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
