using Abp.Application.Services;
using Abp.Runtime.Session;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using System;

namespace Serendip.IK.EventManager
{
    public class EventService : ApplicationService, IEventService
    {
        private readonly IAbpSession abpSession;
        private readonly UserManager userManager;

        public EventService(IAbpSession abpSession, UserManager userManager)
        {
            this.abpSession = abpSession;
            this.userManager = userManager;
        }
        public EventParameter GetEventParameter<TEntity>(string eventName, TEntity entity)
        {
            var eventParam = new EventParameter();

            eventParam.Entity = entity;
            eventParam.EventName = eventName;
            eventParam.EntityName = entity.GetType().FullName;
            eventParam.ModelName = eventName.Split('.')[0];
            eventParam.ActionName = eventName.Split('.')[1];
            eventParam.Date = DateTime.UtcNow;

            if (entity is IMustHaveOwner)
            {
                //eventParam.OwnerId = ((IMustHaveOwner)entity).OwnerId;
                //eventParam.OwnerGroupId = ((IMustHaveOwner)entity).OwnerGroupId;
            }

            if (entity is IMayHaveOwner)
            {
                eventParam.OwnerId = ((IMayHaveOwner)entity).OwnerId;
                eventParam.OwnerGroupId = ((IMayHaveOwner)entity).OwnerGroupId;
            }

            if (entity is IAuthorizedModel)
            {
                eventParam.AuthorizeLevel = ((IAuthorizedModel)entity).AuthorizeLevel;
            }

            var id = entity.GetType().GetProperty("Id");
            if (id != null && id.PropertyType == typeof(long))
            {
                eventParam.Id = long.Parse(id.GetValue(entity).ToString());
            }

            var name = entity.GetType().GetProperty("Name")?.GetValue(entity);
            if (name != null)
            {
                eventParam.Name = name.ToString();
            }

            var fullName = entity.GetType().GetProperty("FullName")?.GetValue(entity);
            if (fullName != null)
            {
                eventParam.Name = fullName.ToString();
            }

            var subject = entity.GetType().GetProperty("Subject")?.GetValue(entity);
            if (subject != null)
            {
                eventParam.Name = subject.ToString();
            }

            var title = entity.GetType().GetProperty("Title")?.GetValue(entity);
            if (title != null)
            {
                eventParam.Name = title.ToString();
            }

            eventParam.Url = $"{eventParam.ModelName}_view" + new { Id = eventParam.Id };

            eventParam.UserId = abpSession.UserId;

            eventParam.TenantId = abpSession.TenantId;

            if (eventParam.UserId.HasValue)
            {
                eventParam.UserName = userManager.GetCurrentUserName().GetAwaiter().GetResult();
            }

            return eventParam;
        }
    }
}
