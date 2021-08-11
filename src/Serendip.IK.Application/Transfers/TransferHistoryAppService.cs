using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Notifications;
using Serendip.IK.Authorization;
using Serendip.IK.Notification;
using Serendip.IK.Transfers.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Transfers
{
    public class TransferHistoryAppService : CoreAsyncCrudAppService<TransferHistory, TransferHistoryDto, long>, ITransferHistoryAppService
    {
        private INotificationPublisher _notificationPublisher;
        private ISettingManager _settingManager;
        private ILocalizationManager _localizationManager;
        private readonly ISuratNotificationService SuratNotificationService;

        public TransferHistoryAppService(IRepository<TransferHistory, long> repository
            , INotificationPublisher notificationPublisher
            , ISettingManager settingManager
            , ILocalizationManager localizationManager,
            ISuratNotificationService SuratNotificationService) : base(repository)
        {
            _notificationPublisher = notificationPublisher;
            _settingManager = settingManager;
            _localizationManager = localizationManager;
            this.SuratNotificationService = SuratNotificationService;
        }




        //[AbpAuthorize(PermissionNames.transfer_view)]
        //public async Task Transfer(TransferHistoryDto dto, bool authUser, bool authUserGroup)
        //{
        //    TransferHistory history = ObjectMapper.Map<TransferHistory>(dto);
        //    history.Date = DateTime.UtcNow;
        //    history.TenantId = AbpSession.TenantId.Value;

        //    //    var user = _userRepository.Get(AbpSession.UserId.Value);
        //    //    var notifData = new LocalizableMessageNotificationData(new LocalizableString($"{dto.EntityType}_{NotificationTypes.TRANSFER_ACTION_NAME}", CoreConsts.LocalizationSourceName));

        //    //if (dto.EntityType == ModelTypes.LEAD)
        //    //{
        //    //    Lead entity = _leadRepository.Get(dto.EntityId);

        //    //    history.FromUserId = entity.OwnerId;
        //    //    history.FromUserGroupId = entity.OwnerGroupId;

        //    //    entity.OwnerId = dto.ToUserId;
        //    //    entity.OwnerGroupId = dto.ToUserGroupId;
        //    //    entity.TenantId = AbpSession.TenantId.Value;
        //    //    _leadRepository.Update(entity);

        //    //    notifData["detail"] = String.Format(_localizationManager.GetString(CoreConsts.LocalizationSourceName, "TransferNotificationDetail"), entity.Title, "Lead");
        //    //    notifData["url"] = UrlGenerator.Url("lead_view", new { Id = entity.Id });
        //    //    notifData["footnote"] = $"{user.Name} {user.Surname} {DateFormatter.FormatDateTime(DateTime.UtcNow)}";
        //    //    var leadAppService = IocManager.Instance.Resolve<ILeadAppService>();
        //    //    await leadAppService.PublishEventAsync(EventNames.LEAD_TRANSFERED, entity);
        //    //}
        //    //else if (dto.EntityType == ModelTypes.OPPORTUNITY)
        //    //{
        //    //    Opportunity entity = _opportunityRepository.Get(dto.EntityId);

        //    //    history.FromUserId = entity.OwnerId;
        //    //    history.FromUserGroupId = entity.OwnerGroupId;

        //    //    entity.OwnerId = dto.ToUserId;
        //    //    entity.OwnerGroupId = dto.ToUserGroupId;
        //    //    entity.TenantId = AbpSession.TenantId.Value;
        //    //    _opportunityRepository.Update(entity);

        //    //    notifData["detail"] = String.Format(_localizationManager.GetString(CoreConsts.LocalizationSourceName, "TransferNotificationDetail"), entity.Title, _localizationManager.GetString(CoreConsts.LocalizationSourceName, "Opportunity"));
        //    //    notifData["url"] = UrlGenerator.Url("opportunity_view", new { Id = entity.Id });
        //    //    notifData["footnote"] = $"{user.Name} {user.Surname} {DateFormatter.FormatDateTime(DateTime.UtcNow)}";
        //    //    var opportunityAppService = IocManager.Instance.Resolve<IOpportunityAppService>();
        //    //    await opportunityAppService.PublishEventAsync(EventNames.OPPORTUNITY_TRANSFERED, entity);
        //    //}
        //    //else if (dto.EntityType == ModelTypes.CUSTOMER)
        //    //{
        //    //    Account entity = _customerRepository.Get(dto.EntityId);

        //    //    history.FromUserId = entity.OwnerId;
        //    //    history.FromUserGroupId = entity.OwnerGroupId;

        //    //    entity.OwnerId = dto.ToUserId;
        //    //    entity.OwnerGroupId = dto.ToUserGroupId;
        //    //    entity.TenantId = AbpSession.TenantId.Value;
        //    //    _customerRepository.Update(entity);
        //    //    notifData["detail"] = String.Format(_localizationManager.GetString(CoreConsts.LocalizationSourceName, "TransferNotificationDetail"), entity.Name, _localizationManager.GetString(CoreConsts.LocalizationSourceName, "Account"));
        //    //    notifData["url"] = UrlGenerator.Url("customer_view", new { Id = entity.Id });
        //    //    notifData["footnote"] = $"{user.Name} {user.Surname} {DateFormatter.FormatDateTime(DateTime.UtcNow)}";
        //    //    var accountAppService = IocManager.Instance.Resolve<IAccountAppService>();
        //    //    await accountAppService.PublishEventAsync(EventNames.ACCOUNT_TRANSFERED, entity);
        //    //}
        //    //else if (dto.EntityType == ModelTypes.CONTACT)
        //    //{
        //    //    Contact entity = _contactRepository.Get(dto.EntityId);

        //    //    history.FromUserId = entity.OwnerId;
        //    //    history.FromUserGroupId = entity.OwnerGroupId;

        //    //    entity.OwnerId = dto.ToUserId;
        //    //    entity.OwnerGroupId = dto.ToUserGroupId;
        //    //    entity.TenantId = AbpSession.TenantId.Value;
        //    //    _contactRepository.Update(entity);

        //    //    notifData["detail"] = String.Format(_localizationManager.GetString(CoreConsts.LocalizationSourceName, "TransferNotificationDetail"), entity.FullName, _localizationManager.GetString(CoreConsts.LocalizationSourceName, "Contact"));
        //    //    notifData["url"] = UrlGenerator.Url("contact_view", new { Id = entity.Id });
        //    //    notifData["footnote"] = $"{user.Name} {user.Surname} {DateFormatter.FormatDateTime(DateTime.UtcNow)}";
        //    //    var contactAppService = IocManager.Instance.Resolve<IContactAppService>();
        //    //    await contactAppService.PublishEventAsync(EventNames.CONTACT_TRANSFERED, entity);
        //    //}
        //    var userIdentifiers = new List<Abp.UserIdentifier>();

        //    if (Convert.ToBoolean(_settingManager.GetSettingValueForTenant("Serendip.IK.TransferNotificationForUser", AbpSession.TenantId.Value)))
        //    {
        //        userIdentifiers.Add(new Abp.UserIdentifier(AbpSession.TenantId.Value, dto.ToUserId));
        //    }

        //    if (Convert.ToBoolean(_settingManager.GetSettingValueForTenant("Serendip.IK.TransferNotificationForUserGroupManager", AbpSession.TenantId.Value)))
        //    {
        //        //var managerId = _userGroupRepository.Get(dto.ToUserGroupId).ManagerId;
        //        //if (managerId.HasValue && !userIdentifiers.Select(x => x.UserId).Contains(managerId.Value))
        //        //{
        //        //    userIdentifiers.Add(new Abp.UserIdentifier(AbpSession.TenantId.Value, managerId.Value));
        //        //}
        //    }

        //    RemoveAuthorize(history, authUser, authUserGroup);
        //    Repository.Insert(history);
        //    CreateAuthorize(dto);

        //    //_notificationPublisher.Publish(NotificationTypes.GetType(dto.EntityType, NotificationTypes.TRANSFER_ACTION_NAME), data: notifData, userIds: userIdentifiers.ToArray());
        //    //SuratNotificationService.PrepareNotification(notifData, AbpSession.TenantId.Value, AbpSession.UserId.Value, userIdentifiers.Select(s => s.UserId.ToString()).ToArray());
        //    //SuratNotificationService.PrepareNotification(notifData, AbpSession.TenantId.Value, AbpSession.UserId.Value, userIdentifiers.Select(s => s.UserId.ToString()).ToArray());

        //}

        //private void RemoveAuthorize(TransferHistory dto, bool authUser, bool authUserGroup)
        //{
        //    //if (!authUser && !authUserGroup)
        //    //{
        //    //    _authorizeAppService.RemoveAuthorize(dto.FromUserGroupId, dto.FromUserId, dto.EntityId, dto.EntityType);
        //    //}
        //    //else if (!authUser)
        //    //{
        //    //    _authorizeAppService.RemoveAuthorize(null, dto.FromUserId, dto.EntityId, dto.EntityType);
        //    //}
        //    //else if (!authUserGroup)
        //    //{
        //    //    _authorizeAppService.RemoveAuthorize(dto.FromUserGroupId, null, dto.EntityId, dto.EntityType);
        //    //}
        //}

        //[AbpAuthorize(PermissionNames.transfer_create)]
        //public void CreateFirstHistory(TransferHistoryDto dto)
        //{
        //    dto.Date = DateTime.UtcNow;
        //    //dto.TenantId = AbpSession.TenantId.Value;
        //    Repository.Insert(ObjectMapper.Map<TransferHistory>(dto));
        //}

        //private void CreateAuthorize(TransferHistoryDto dto)
        //{
        //    //AuthorizeDto forUser = new AuthorizeDto
        //    //{
        //    //    ModelId = dto.EntityId,
        //    //    ModelName = dto.EntityType,
        //    //    UserId = dto.ToUserId
        //    //};

        //    //AuthorizeDto forUserGroup = new AuthorizeDto
        //    //{
        //    //    ModelId = dto.EntityId,
        //    //    ModelName = dto.EntityType,
        //    //    UserGroupId = dto.ToUserGroupId
        //    //};

        //    //_authorizeAppService.SetAuthorize(new List<AuthorizeDto> { forUser, forUserGroup });
        //}

        ////public async Task<PagedResultDto<TransferHistoryDto>> GetAllHistories(Guid id, TransferHistoryFilterDto filter)
        ////{
        ////    //var transferHistory = Repository.GetAllIncluding(x=>x.ToUserGroup,x=> x.FromUserGroup , x => x.ToUser, x => x.FromUser).Where(x => x.EntityId == id);
        ////    //int count = transferHistory.Count();
        ////    //transferHistory = ApplyPaging(transferHistory, filter);
        ////    //transferHistory = ApplySorting(transferHistory, filter);
        ////    //var data = await transferHistory.Select(x => new TransferHistoryDto
        ////    //{
        ////    //    Id = x.Id,
        ////    //    FromUserId=x.FromUserId,
        ////    //    ToUserId = x.ToUserId,
        ////    //    FromUser= ObjectMapper.Map<UserDto>(x.FromUser),
        ////    //    ToUser=ObjectMapper.Map<UserDto>(x.ToUser),
        ////    //    FromUserGroup = ObjectMapper.Map<UserGroupDto>(x.FromUserGroup),
        ////    //    ToUserGroup = ObjectMapper.Map<UserGroupDto>(x.ToUserGroup),
        ////    //    Description = x.Description,
        ////    //    EntityId = x.EntityId,
        ////    //    Date = x.Date,
        ////    //    FromUserGroupId = x.FromUserGroupId,
        ////    //    ToUserGroupId = x.ToUserGroupId
        ////    //}).ToListAsync();
        ////    return new PagedResultDto<TransferHistoryDto>
        ////    {
        ////        TotalCount = 1,
        ////        Items = null
        ////    };
        ////}

        //public async Task<PagedResultDto<TransferHistoryDto>> GetAllHistories(long id, TransferHistoryFilterDto filter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}