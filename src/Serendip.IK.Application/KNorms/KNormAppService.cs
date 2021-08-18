using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.KNormDetails;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.KSubes;
using Serendip.IK.Notification;
using Serendip.IK.Users;
using Serendip.IK.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KNorms
{

    public class KNormAppService : AsyncCrudAppService<KNorm, KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>, IKNormAppService
    {

        #region Constructor
        private IUserAppService _userService;
        private readonly IAbpSession _session;
        public IEventBus EventBus { get; set; }
        private INotificationService _notificationService;
        private IKNormDetailAppService _kNormDetailAppService;
        private INotificationPublisherService _notificationPublisherService;
        private INotificationSubscriptionManager _notificationSubscriptionManager;
        private IKSubeAppService _kSubeAppService;


        public KNormAppService(
            IAbpSession session,
            IUserAppService userService,
            IRepository<KNorm, long> repository,
            INotificationService notificationService,
            IKNormDetailAppService kNormDetailAppService,
            INotificationPublisherService notificationPublisherService,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IKSubeAppService kSubeAppService
          ) : base(repository)
        {
            _session = session;
            _userService = userService;
            EventBus = NullEventBus.Instance;
            _notificationService = notificationService;
            _kNormDetailAppService = kNormDetailAppService;
            _notificationPublisherService = notificationPublisherService;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _kSubeAppService = kSubeAppService;
        }
        #endregion


        #region GetBolgeNormsAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<PagedResultDto<KNormDto>> GetBolgeNormsAsync(PagedKNormResultRequestDto input)
        {
            var kNormList = await Repository.GetAllListAsync();

            List<KNormDto> kNorms = new();

            foreach (var norm in kNormList)
            {
                KNormDto kNormDto = new KNormDto();

                var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = norm.SubeObjId });
                var bolge = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = long.Parse(sube.BagliOlduguSube_ObjId) });

                kNormDto.Id = norm.Id;
                kNormDto.Pozisyon = norm.Pozisyon;
                kNormDto.Aciklama = norm.Aciklama;
                kNormDto.TalepTuru = norm.TalepTuru;
                kNormDto.NormStatus = norm.NormStatus;
                kNormDto.TalepNedeni = norm.TalepNedeni;
                kNormDto.TalepDurumu = norm.TalepDurumu;
                kNormDto.YeniPozisyon = norm.YeniPozisyon;
                kNormDto.CreationTime = norm.CreationTime;
                kNormDto.SubeObjId = norm.SubeObjId.ToString();
                kNormDto.PersonelId = norm.PersonelId != null ? norm.PersonelId.Value.ToString() : null;
                kNormDto.SubeAdi = sube.Adi;
                kNormDto.BolgeAdi = bolge.Adi;

                kNorms.Add(kNormDto);
            }

            var result = kNorms.WhereIf(input.Keyword != "",
                x => x.Pozisyon.ToLower().Contains(input.Keyword) ||
                x.TalepNedeni.GetDisplayName().Contains(input.Keyword.Replace("ı", "i")) ||
                x.TalepDurumu.GetDisplayName().Contains(input.Keyword) ||
                x.NormStatus.GetDisplayName().Contains(input.Keyword) ||
                x.CreationTime.ToLongDateString().Contains(input.Keyword) ||
                x.TalepTuru.GetDisplayName().Contains(input.Keyword));

            return new PagedResultDto<KNormDto>
            {
                TotalCount = kNorms.Count(),
                Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };
        }
        #endregion

        #region GetBolgeNormsCountAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<List<KNormCountDto>> GetBolgeNormsCountAsync()
        {
            var kNormList = await Repository.GetAllListAsync();
            return ObjectMapper.Map<List<KNormCountDto>>(kNormList);


        }
        #endregion

        #region GetSubeNormsAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<PagedResultDto<KNormDto>> GetSubeNormsAsync(PagedKNormResultRequestDto input)
        {
            var kNormList = await Repository.GetAllListAsync();

            long id = long.Parse(input.BolgeId);
            var data = kNormList.Where(x => x.BagliOlduguSubeObjId == id).Select(x => new KNormDto
            {
                Id = x.Id,
                TalepDurumu = x.TalepDurumu,
                TalepNedeni = x.TalepNedeni,
                TalepTuru = x.TalepTuru,
                Pozisyon = x.Pozisyon,
                YeniPozisyon = x.YeniPozisyon,
                PersonelId = x.PersonelId != null ? x.PersonelId.Value.ToString() : null,
                Aciklama = x.Aciklama,
                SubeObjId = x.SubeObjId.ToString(),
                NormStatus = x.NormStatus,
                BagliOlduguSubeObjId = x.BagliOlduguSubeObjId,
                CreationTime = x.CreationTime

            }).WhereIf(input.Keyword != "",
                x => x.Pozisyon.ToLower().Contains(input.Keyword) ||
                x.Nedeni.ToLower().Contains(input.Keyword.Replace("ı", "i")) ||
                x.TalepDurumu.GetDisplayName().Contains(input.Keyword) ||
                x.NormStatus.GetDisplayName().Contains(input.Keyword) ||
                x.CreationTime.ToLongDateString().Contains(input.Keyword) ||
                x.Turu.ToLower().Contains(input.Keyword));

            return new PagedResultDto<KNormDto>
            {
                TotalCount = data.Count(),
                Items = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };

        }
        #endregion

        #region GetSubeNormsCountAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<List<KNormCountDto>> GetSubeNormsCountAsync(PagedKNormResultRequestDto input)
        {
            var kNormList = await Repository.GetAllListAsync();
            return ObjectMapper.Map<List<KNormCountDto>>(kNormList);


        }
        #endregion

        #region GetSubeDetailNormsAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<PagedResultDto<KNormDto>> GetSubeDetailNormsAsync(PagedKNormResultRequestDto input)
        {
            long id = long.Parse(input.Id);
            var kNormList = await Repository.GetAllListAsync(x => x.SubeObjId == id);

            List<KNormDto> kNorms = new();
            foreach (var norm in kNormList)
            {
                KNormDto kNormDto = new KNormDto();

                var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = norm.SubeObjId });
                var bolge = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = long.Parse(sube.BagliOlduguSube_ObjId) });

                kNormDto.Id = norm.Id;
                kNormDto.Pozisyon = norm.Pozisyon;
                kNormDto.Aciklama = norm.Aciklama;
                kNormDto.TalepTuru = norm.TalepTuru;
                kNormDto.NormStatus = norm.NormStatus;
                kNormDto.TalepNedeni = norm.TalepNedeni;
                kNormDto.TalepDurumu = norm.TalepDurumu;
                kNormDto.YeniPozisyon = norm.YeniPozisyon;
                kNormDto.CreationTime = norm.CreationTime;
                kNormDto.SubeObjId = norm.SubeObjId.ToString();
                kNormDto.PersonelId = norm.PersonelId != null ? norm.PersonelId.Value.ToString() : null;
                kNormDto.SubeAdi = sube.Adi;
                kNormDto.BolgeAdi = bolge.Adi;
                kNorms.Add(kNormDto);
            }

            var result = kNorms.WhereIf(input.Keyword != "",
                x => x.Pozisyon.ToLower().Contains(input.Keyword) ||
                x.Nedeni.ToLower().Contains(input.Keyword.Replace("ı", "i")) ||
                x.TalepDurumu.GetDisplayName().Contains(input.Keyword) ||
                x.NormStatus.GetDisplayName().Contains(input.Keyword) ||
                x.CreationTime.ToLongDateString().Contains(input.Keyword) ||
                x.TalepTuru.GetDisplayName().Contains(input.Keyword)).ToList();


            return new PagedResultDto<KNormDto>
            {
                TotalCount = result.Count(),
                Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
            };

        }
        #endregion

        #region GetSubeDetailNormsCountAsync
        [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<List<KNormCountDto>> GetSubeDetailNormsCountAsync(PagedKNormResultRequestDto input)
        {
            long id = long.Parse(input.Id);
            var kNormList = await Repository.GetAllListAsync(x => x.SubeObjId == id);
            return ObjectMapper.Map<List<KNormCountDto>>(kNormList);
        }
        #endregion

        #region Create

        [AbpAuthorize(PermissionNames.knorm_create)]
        public override async Task<KNormDto> CreateAsync(CreateKNormDto input)
        {
            try
            {
                input.NormStatus = NormStatus.Beklemede;
                input.TalepDurumu = (TalepDurumu)Enum.Parse(typeof(TalepDurumu), input.Mails[0].GMYType != KHierarchies.GMYType.None ? $"{input.Mails[0].GMYType}_{input.Mails[0].NormalizedTitle}".ToUpper() : input.Mails[0].NormalizedTitle);
                var entityDto = await base.CreateAsync(input);

                foreach (var mail in input.Mails.Select((m, x) => (m, x)))
                {
                    CreateKNormDetailDto dto = new CreateKNormDetailDto();
                    dto.KNormId = entityDto.Id;
                    dto.UserId = _userService.GetEmailById(mail.m.Mail);
                    dto.TalepDurumu = (TalepDurumu)Enum.Parse(typeof(TalepDurumu), mail.m.GMYType != KHierarchies.GMYType.None ? $"{mail.m.GMYType}_{mail.m.NormalizedTitle}".ToUpper() : mail.m.NormalizedTitle);
                    dto.OrderNo = mail.x;
                    await _kNormDetailAppService.CreateAsync(dto);
                }

                _notificationSubscriptionManager.Subscribe(
                   new UserIdentifier(AbpSession.TenantId, AbpSession.UserId.Value),
                  NotificationTypes.GetType(ModelTypes.KNORM,
                  NotificationTypes.CHANGES_ACTION_NAME),
                  new EntityIdentifier(typeof(KNorm), entityDto.Id));

                await _notificationPublisherService.KNormAdded(entityDto);

                EventBus.Trigger(GetEventParameter(new EventHandlerEto<KNorm>
                {
                    EventName = EventNames.KNORM_CREATED,
                    Entity = MapToEntity(input),
                    LogType = ActivityLoggerTypes.ITEM_ADDED,
                    DisplayKey = "Norm_Added"
                }));

                return entityDto;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region SetStatusAsync
        [AbpAuthorize(PermissionNames.knorm_statuschange)]
        public async Task<KNormDto> SetStatusAsync([FromBody] KNormDto input)
        {
            try
            {
                var norm = await Repository.GetAsync(input.Id);
                long userId = _session.GetUserId();
                var user = await _userService.GetAsync(new EntityDto<long> { Id = userId });
                norm.TalepDurumu = await _kNormDetailAppService.GetNextStatu(input.Id);


                if (input.NormStatus == NormStatus.Iptal)
                {
                    norm.NormStatus = NormStatus.Iptal;
                }

                if (!await _kNormDetailAppService.CheckStatus(input.Id))
                {
                    norm.NormStatus = NormStatus.Onaylandi;
                }

                var result = await Repository.UpdateAsync(norm);
                await _notificationPublisherService.KNormStatusChanged(ObjectMapper.Map<KNormDto>(result));

                EventBus.Trigger(GetEventParameter(new EventHandlerEto<KNorm>
                {
                    EventName = EventNames.KNORM_STATUS_CHANGED,
                    Entity = ObjectMapper.Map<KNorm>(result),
                    LogType = ActivityLoggerTypes.ITEM_ADDED,
                    DisplayKey = "Norm_Status_Changed"
                }));

                return ObjectMapper.Map<KNormDto>(norm);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region EventParameter
        [DisableValidation]
        public virtual EventParameter GetEventParameter(EventHandlerEto<KNorm> eto)
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

            if (eto.Entity is IAuthorizedModel)
            {
                eventParam.AuthorizeLevel = ((IAuthorizedModel)eto.Entity).AuthorizeLevel;
            }

            var id = eto.Entity.GetType().GetProperty("Id");
            if (id != null && id.PropertyType == typeof(Int64))
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

            //eventParam.Url = UrlGenerator.FullUrl($"{eventParam.ModelName}_view");
            eventParam.Url += eventParam.Id;
            eventParam.UserId = AbpSession.UserId;

            if (eventParam.UserId.HasValue)
            {
                var userManager = IocManager.Instance.Resolve<UserManager>();
                eventParam.UserName = userManager.GetCurrentUserName().Result;
            }

            return eventParam;
        }
        #endregion
    }
}

