using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Serendip.IK.ActivityLoggers;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Transfers;
using Serendip.IK.Transfers.Dto;
using System;

namespace Serendip.IK
{
    public class CoreAsyncCrudAppService<TEntity, TDto, TPrimaryKey>
        : AsyncCrudAppService<TEntity, TDto, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDto : IEntityDto<TPrimaryKey>
    {



        protected const string ROW_LIMIT = "Row_Limit";
        //IRepository<TransferHistory, long> _transferHistoryRepo;
        public IEventBus EventBus { get; set; }
        public CoreAsyncCrudAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
            EventBus = NullEventBus.Instance;
        }

        public virtual void CheckRowLimit(string featureKey)
        {
            if (!string.IsNullOrEmpty(featureKey))
            {
                //var type = typeof(IMustHaveRowLimit);
                //if (type.IsAssignableFrom(typeof(TEntity)))
                //{
                //    var applicationLimitRepository = IocManager.Instance.Resolve<IRepository<Common.ApplicationLimit, Guid>>();
                //    var applicationLimit = applicationLimitRepository.GetAll().FirstOrDefault(x => x.TenantId == AbpSession.TenantId) ??
                //        new Common.ApplicationLimit();
                //    if (applicationLimit.TotalRows >= FeatureChecker.GetValue(featureKey).To<int>())
                //        throw new Exception("");  // throw new LimitExceedeedException(nameof(TEntity) + L("LimitExceedeed"));
                //}
            }
        }

        public virtual void CheckSizeLimit(int currentSize)
        {
            if (currentSize >= FeatureChecker.GetValue("Size_Limit").To<int>())
                throw new Exception("");  // new LimitExceedeedException(nameof(TEntity) + L("LimitExceedeed"));
        }

        [DisableValidation]
        public virtual EventParameter GetEventParameter(EventHandlerEto<TEntity> eto)
        {

            var eventParam = new EventParameter();
            eventParam.Log = new LogParameter
            {
                DisplayValues = eto.DisplayValues,
                ReferenceID = eto.ReferenceID,
                ReferenceModel = eto.ReferenceModel,
                DisplayKey = eto.DisplayKey,
                LogType = eto.LogType
            };
            eventParam.Entity = eto.Entity;
            eventParam.EventName = eto.EventName;
            eventParam.EntityName = eto.Entity.GetType().FullName;
            eventParam.ModelName = eto.EventName.Split('.')[0];
            eventParam.ActionName = eto.EventName.Split('.')[1];
            eventParam.Date = DateTime.UtcNow;

            //if (eto.Entity is IMustHaveRowLimit)
            //    eventParam.IsLimitedEntity = true;

            if (eto.Entity is IMustHaveOwner)
            {
                //eventParam.OwnerId = ((IMustHaveOwner)eto.Entity).OwnerId;
                //eventParam.OwnerGroupId = ((IMustHaveOwner)eto.Entity).OwnerGroupId;
            }

            if (eto.Entity is IMayHaveOwner)
            {
                eventParam.OwnerId = ((IMayHaveOwner)eto.Entity).OwnerId;
                eventParam.OwnerGroupId = ((IMayHaveOwner)eto.Entity).OwnerGroupId;
            }

            if (eto.Entity is IAuthorizedModel)
            {
                eventParam.AuthorizeLevel = ((IAuthorizedModel)eto.Entity).AuthorizeLevel;
            }

            var id = eto.Entity.GetType().GetProperty("Id");
            if (id != null && id.PropertyType == typeof(long))
            {
                eventParam.Id = long.Parse(id.GetValue(eto.Entity).ToString());
            }

            var name = eto.Entity.GetType().GetProperty("Name")?.GetValue(eto.Entity);
            if (name != null)
            {
                eventParam.Name = name.ToString();
            }

            var fullName = eto.Entity.GetType().GetProperty("FullName")?.GetValue(eto.Entity);
            if (fullName != null)
            {
                eventParam.Name = fullName.ToString();
            }

            var subject = eto.Entity.GetType().GetProperty("Subject")?.GetValue(eto.Entity);
            if (subject != null)
            {
                eventParam.Name = subject.ToString();
            }

            var title = eto.Entity.GetType().GetProperty("Title")?.GetValue(eto.Entity);
            if (title != null)
            {
                eventParam.Name = title.ToString();
            }

            eventParam.Url = "";//UrlGenerator.FullUrl($"{eventParam.ModelName}_view");
            eventParam.Url += eventParam.Id;
            eventParam.UserId = AbpSession.UserId;

            if (eventParam.UserId.HasValue)
            {
                var userManager = IocManager.Instance.Resolve<UserManager>();
                eventParam.UserName = userManager.GetCurrentUserName().Result;
            }

            return eventParam;
        }

        public IActivityLoggerAppService ActivityLoggerAppService { get; }

        public void SaveLog(string name, string displayKey, string modelName = "", string modelId = "", string referenceModel = "", string referenceId = "", string[] displayValues = null)
        {
            var ActivityLoggerAppService = IocManager.Instance.Resolve<IActivityLoggerAppService>();
            //ActivityLoggerAppService.SaveLog(new ActivityLoggers.Dto.ActivityLoggerDto
            //{
            //    Name = name,
            //    DisplayKey = displayKey,
            //    ModelName = modelName,
            //    ModelId = modelId,
            //    ReferenceModel = referenceModel,
            //    ReferenceID = referenceId,
            //    DisplayValues = displayValues
            //});
        }

        public void CreateFirstHistory(TransferHistoryDto dto)
        {
            var _transferHistoryRepo = IocManager.Instance.Resolve<IRepository<TransferHistory, long>>();
            dto.Date = DateTime.UtcNow;
            //dto.TenantId = AbpSession.TenantId.Value;
            _transferHistoryRepo.Insert(ObjectMapper.Map<TransferHistory>(dto));
        }

    }

    public class EventHandlerEto<TEntity> where TEntity : class
    {
        public string EventName { get; set; }

        public TEntity Entity { get; set; }

        public string LogType { get; set; }
        public string DisplayKey { get; set; }

        public string ReferenceModel { get; set; }
        public string ReferenceID { get; set; }

        public string[] DisplayValues { get; set; }
    }
}
