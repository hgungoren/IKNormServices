using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Authorization;
using Serendip.IK.KNormDetails;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.KPersonels;
using Serendip.IK.KSubes;
using Serendip.IK.Nodes.dto;
using Serendip.IK.Notification;
using Serendip.IK.Units;
using Serendip.IK.Users;
using Serendip.IK.Users.Dto;
using Serendip.IK.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Serendip.IK.KNorms
{

    public class KNormAppService : AsyncCrudAppService<KNorm, KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>, IKNormAppService
    {
        #region Constructor
        private IUserAppService _userAppService;
        private readonly IAbpSession _abpSession;
        public IEventBus EventBus { get; set; }
        private INotificationService _notificationService;
        private IKNormDetailAppService _kNormDetailAppService;
        private INotificationPublisherService _notificationPublisherService;
        private INotificationSubscriptionManager _notificationSubscriptionManager;
        private IKSubeAppService _kSubeAppService;
        private IKPersonelAppService _kPersonelAppService;
        private readonly IUnitAppService _unitAppService;

        public KNormAppService(
            IAbpSession abpSession,
            IUserAppService userAppService,
            IRepository<KNorm, long> repository,
            INotificationService notificationService,
            IKNormDetailAppService kNormDetailAppService,
            INotificationPublisherService notificationPublisherService,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IKPersonelAppService kPersonelAppService,
            IKSubeAppService kSubeAppService,
                IUnitAppService unitAppService
          ) : base(repository)
        {
            _abpSession = abpSession;
            _userAppService = userAppService;
            EventBus = NullEventBus.Instance;
            _notificationService = notificationService;
            _kNormDetailAppService = kNormDetailAppService;
            _notificationPublisherService = notificationPublisherService;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _kSubeAppService = kSubeAppService;
            _kPersonelAppService = kPersonelAppService;
            _unitAppService = unitAppService;
        }
        #endregion

        #region GetBolgeNormsAsync
        // [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<PagedResultDto<KNormDto>> GetBolgeNormsAsync(PagedKNormResultRequestDto input)
        {

            try
            {
                // TODO : Bu alan düzenlenecek
                var userId = _abpSession.GetUserId();
                var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                var roles = user.RoleNames;
                List<KNorm> kNormList;


                if (roles.Contains("GENELMUDURLUK") || roles.Contains("ADMIN"))
                {
                    kNormList = await Repository.GetAllListAsync();
                }
                else
                {
                    kNormList = await Repository.GetAllListAsync(x => x.BagliOlduguSubeObjId == user.CompanyObjId || x.SubeObjId == user.CompanyObjId);
                }

                try
                {
                    List<KNormDto> kNorms = new();

                    foreach (var norm in kNormList)
                    {
                        KNormDto kNormDto = new KNormDto();

                        var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = norm.SubeObjId });
                        var bolge = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = long.Parse(sube.BagliOlduguSube_ObjId) });

                        if (norm.PersonelId.HasValue)
                        {
                            long personelId = norm.PersonelId != null ? norm.PersonelId.Value : 0;
                            var personel = await _kPersonelAppService.GetById(personelId);

                            kNormDto.PersonelId = personelId.ToString();
                            kNormDto.PersonelAdi = $"{personel.Ad} {personel.Soyad}";
                        }


                        kNormDto.Id = norm.Id;
                        kNormDto.SubeAdi = sube.Adi;
                        kNormDto.BolgeAdi = bolge.Adi;
                        kNormDto.Pozisyon = norm.Pozisyon;
                        kNormDto.Aciklama = norm.Aciklama;
                        kNormDto.TalepTuru = norm.TalepTuru;
                        kNormDto.NormStatus = norm.NormStatus;
                        kNormDto.TalepNedeni = norm.TalepNedeni;
                        kNormDto.TalepDurumu = norm.TalepDurumu;
                        kNormDto.YeniPozisyon = norm.YeniPozisyon;
                        kNormDto.CreationTime = norm.CreationTime;
                        kNormDto.SubeObjId = norm.SubeObjId.ToString();
                        kNorms.Add(kNormDto);
                    }

                    var result = kNorms.WhereIf(input.Keyword != "",
                        x => x.Pozisyon.ToLower().Contains(input.Keyword) ||
                        x.TalepNedeni.GetDisplayName(true).Contains(input.Keyword, StringComparison.OrdinalIgnoreCase) ||
                        //x.TalepNedeni.GetDisplayName(true).Contains(input.Keyword.Replace("i", "ı")) ||
                        x.TalepDurumu.GetDisplayName(true).Contains(input.Keyword) ||
                        x.NormStatus.GetDisplayName(true).Contains(input.Keyword) ||
                        x.CreationTime.ToLongDateString().Contains(input.Keyword) ||
                        x.TalepTuru.GetDisplayName(true).Contains(input.Keyword))
                           .WhereIf((input.Start != null && input.End != null), x =>
                                                         x.CreationTime.Year >= input.Start.Value.Year &&
                                                         x.CreationTime.Month >= input.Start.Value.Month &&
                                                         x.CreationTime.Day >= input.Start.Value.Day &&
                                                         x.CreationTime.Year <= input.End.Value.Year &&
                                                         x.CreationTime.Month <= input.End.Value.Month &&
                                                         x.CreationTime.Day <= input.End.Value.Day);

                    return new PagedResultDto<KNormDto>
                    {
                        TotalCount = kNorms.Count(),
                        Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
                    };
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region GetBolgeNormsCountAsync
        // [AbpAuthorize(PermissionNames.knorm_view)]
        public async Task<List<KNormCountDto>> GetBolgeNormsCountAsync(PagedKNormResultRequestDto input)
        {
            try
            {
                var userId = _abpSession.GetUserId();
                var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                var roles = user.RoleNames;
                List<KNorm> kNormList;


                var data = await Repository.GetAllListAsync();
                var retVal = data
                           .WhereIf((input.Start != null && input.End != null), x =>
                                                         x.CreationTime.Year >= input.Start.Value.Year &&
                                                         x.CreationTime.Month >= input.Start.Value.Month &&
                                                         x.CreationTime.Day >= input.Start.Value.Day &&
                                                         x.CreationTime.Year <= input.End.Value.Year &&
                                                         x.CreationTime.Month <= input.End.Value.Month &&
                                                         x.CreationTime.Day <= input.End.Value.Day);



                if (roles.Contains("GENELMUDURLUK") || roles.Contains("ADMIN"))
                {
                    kNormList = retVal.ToList();
                }
                else
                {
                    kNormList = retVal.Where(x => x.BagliOlduguSubeObjId == user.CompanyObjId || x.SubeObjId == user.CompanyObjId).ToList();
                }

                return ObjectMapper.Map<List<KNormCountDto>>(kNormList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region GetSubeNormsAsync
        //[AbpAuthorize(
        //    PermissionNames.knorm_view,
        //    PermissionNames.kbolge_employee_list
        //    )
        //]
        public async Task<PagedResultDto<KNormDto>> GetSubeNormsAsync(PagedKNormResultRequestDto input)
        {
            try
            {
                var kNormList = await Repository.GetAllListAsync();

                long id = long.Parse(input.BolgeId);
                var bolge = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = id });
                var data = kNormList.Where(x => x.SubeObjId == id || x.BagliOlduguSubeObjId == id);

                List<KNormDto> kNorms = new();

                foreach (var norm in data)
                {
                    var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = norm.SubeObjId });
                    KNormDto kNorm = new KNormDto();
                    kNorm.Id = norm.Id;
                    kNorm.TalepDurumu = norm.TalepDurumu;
                    kNorm.TalepNedeni = norm.TalepNedeni;
                    kNorm.TalepTuru = norm.TalepTuru;
                    kNorm.Pozisyon = norm.Pozisyon;
                    kNorm.YeniPozisyon = norm.YeniPozisyon;
                    kNorm.PersonelId = norm.PersonelId != null ? norm.PersonelId.Value.ToString() : null;
                    kNorm.Aciklama = norm.Aciklama;
                    kNorm.SubeObjId = norm.SubeObjId.ToString();
                    kNorm.NormStatus = norm.NormStatus;
                    kNorm.BagliOlduguSubeObjId = norm.BagliOlduguSubeObjId;
                    kNorm.CreationTime = norm.CreationTime;
                    kNorm.SubeAdi = sube.Adi;
                    kNorm.BolgeAdi = bolge.Adi;
                    kNorms.Add(kNorm);
                }


                var result = kNorms
                          .WhereIf((input.Start != null && input.End != null), x =>
                                                     x.CreationTime.Year >= input.Start.Value.Year &&
                                                     x.CreationTime.Month >= input.Start.Value.Month &&
                                                     x.CreationTime.Day >= input.Start.Value.Day &&
                                                     x.CreationTime.Year <= input.End.Value.Year &&
                                                     x.CreationTime.Month <= input.End.Value.Month &&
                                                     x.CreationTime.Day <= input.End.Value.Day);


                return new PagedResultDto<KNormDto>
                {
                    TotalCount = result.Count(),
                    Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region GetSubeNormsCountAsync
        // [AbpAuthorize(PermissionNames.knorm_view, PermissionNames.kbolge_employee_list)]
        public async Task<List<KNormCountDto>> GetSubeNormsCountAsync(PagedKNormResultRequestDto input)
        {
            long id = long.Parse(input.BolgeId);

            var kNorms = await Repository.GetAllListAsync(x => x.SubeObjId == id || x.BagliOlduguSubeObjId == id);
            var result = kNorms
                     .WhereIf((input.Start != null && input.End != null), x =>
                                                x.CreationTime.Year >= input.Start.Value.Year &&
                                                x.CreationTime.Month >= input.Start.Value.Month &&
                                                x.CreationTime.Day >= input.Start.Value.Day &&
                                                x.CreationTime.Year <= input.End.Value.Year &&
                                                x.CreationTime.Month <= input.End.Value.Month &&
                                                x.CreationTime.Day <= input.End.Value.Day);

            return ObjectMapper.Map<List<KNormCountDto>>(result);
        }
        #endregion

        #region GetSubeDetailNormsAsync
        //[
        //    AbpAuthorize
        //    (
        //        PermissionNames.knorm_view,
        //        PermissionNames.ksubedetail_norm_request_list
        //    )
        //]
        public async Task<PagedResultDto<KNormDto>> GetSubeDetailNormsAsync(PagedKNormResultRequestDto input)
        {
            try
            {
                long id = 0;
                if (input.Id == "0")
                {
                    id = long.Parse(input.Id);

                    if (id == 0)
                    {
                        var userId = _abpSession.GetUserId();
                        var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                        id = user.CompanyObjId;
                    }
                }
                else
                {
                    id = long.Parse(input.Id);
                }

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
                    x.TalepDurumu.GetDisplayName(true).Contains(input.Keyword) ||
                    x.NormStatus.GetDisplayName(true).Contains(input.Keyword) ||
                    x.CreationTime.ToLongDateString().Contains(input.Keyword) ||
                    x.TalepTuru.GetDisplayName(true).Contains(input.Keyword));


                return new PagedResultDto<KNormDto>
                {
                    TotalCount = result.Count(),
                    Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList()
                };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region GetSubeDetailNormsCountAsync
        //[
        //   AbpAuthorize
        //   (
        //       PermissionNames.knorm_view,
        //       PermissionNames.ksubedetail_norm_request_list
        //   )
        //]
        public async Task<List<KNormCountDto>> GetSubeDetailNormsCountAsync(PagedKNormResultRequestDto input)
        {
            try
            {
                long id = 0;
                if (input.Id == "0")
                {
                    id = long.Parse(input.Id);

                    if (id == 0)
                    {
                        var userId = _abpSession.GetUserId();
                        var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                        id = user.CompanyObjId;
                    }
                }
                else
                {
                    id = long.Parse(input.Id);
                }

                var kNormList = await Repository.GetAllListAsync(x => x.SubeObjId == id);

                return ObjectMapper.Map<List<KNormCountDto>>(kNormList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Create

        // [AbpAuthorize(PermissionNames.knorm_create)]
        public override async Task<KNormDto> CreateAsync(CreateKNormDto input)
        {
            var timer = new Stopwatch();
            timer.Start();

            if (input.SubeObjId == 0)
            {
                var userId = _abpSession.GetUserId();
                var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                input.SubeObjId = user.CompanyObjId;

                // TODO : bağlı olduğu şube obj id dinamik çekilecek
                //var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = userId }); 
                //input.BagliOlduguSubeObjId = long.Parse(sube.BagliOlduguSube_ObjId); 
            }

            input.NormStatus = NormStatus.Beklemede;
            input.TalepDurumu = (TalepDurumu)Enum.Parse(typeof(TalepDurumu), input.Mails[0].GMYType != GMYType.None ? $"{input.Mails[0].GMYType}_{input.Mails[0].NormalizedTitle}".ToUpper() : input.Mails[0].NormalizedTitle);
            var knorm = await base.CreateAsync(input);
            var hierarchy = await _unitAppService.GetByUnit(input.Tip);
            var position = hierarchy.Positions.Where(x => x.Name == input.Pozisyon).FirstOrDefault();
            //var titles = position.Nodes.Where(x => x.Active).Select(n => n.Title).ToArray();


            bool isVisible = true;
            foreach (var mail in input.Mails.Select((m, x) => (m, x)))
            {
                var user = await _userAppService.GetByEmail(mail.m.Mail);

                if (user == null)
                    continue;

                CreateKNormDetailDto dto = new CreateKNormDetailDto();
                dto.KNormId = knorm.Id;
                dto.UserId = user.Id;
                dto.TalepDurumu = (TalepDurumu)Enum.Parse(typeof(TalepDurumu), mail.m.NormalizedTitle);
                dto.OrderNo = mail.x;

                if (isVisible)
                {
                    dto.Visible = true;
                    isVisible = false;
                }

                _kNormDetailAppService.CreateAsync(dto).Wait();
                var node = position.Nodes.FirstOrDefault(x => x.Title == mail.m.Title);
                SubScribeUser(node, knorm.Id, user);


                await _notificationPublisherService.KNormAdded(knorm, user);
            }
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            System.Diagnostics.Trace.WriteLine(foo);
            var kNormEvent = new EventHandlerEto<KNorm>
            {
                EventName = EventNames.KNORM_CREATED,
                Entity = MapToEntity(input),
                LogType = ActivityLoggerTypes.ITEM_ADDED,
                DisplayKey = "Norm_Added",
                ReferenceID = knorm.Id.ToString(),
            };

            EventBus.Trigger(GetEventParameter(kNormEvent));

            return knorm;
        }
        #endregion


        private async void SubScribeUser(NodeDto node, long kNormId, UserDto user)
        {

            if (node == null)
            {
                return;
            }

            var userIdentifier = new UserIdentifier(AbpSession.TenantId, user.Id);

            if (node.Mail)
            {
                var mailNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL);
                _notificationSubscriptionManager.Subscribe(userIdentifier, mailNotification, new EntityIdentifier(typeof(KNorm), kNormId));

            }
            if (node.MailStatusChange)
            {
                var mainStatusChangeNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS_MAIL);
                _notificationSubscriptionManager.Subscribe(userIdentifier, mainStatusChangeNotification, new EntityIdentifier(typeof(KNorm), kNormId));
            }
            if (node.PushNotificationWeb)
            {
                var webNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_WEB);
                _notificationSubscriptionManager.Subscribe(userIdentifier, webNotification, new EntityIdentifier(typeof(KNorm), kNormId));
            }
            if (node.PushNotificationWebStatusChange)
            {
                var webStatusChangeNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS_WEB);
                _notificationSubscriptionManager.Subscribe(userIdentifier, webStatusChangeNotification, new EntityIdentifier(typeof(KNorm), kNormId));
            }
            if (node.PushNotificationPhone)
            {
                var phoneNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_PHONE);
                _notificationSubscriptionManager.Subscribe(userIdentifier, phoneNotification, new EntityIdentifier(typeof(KNorm), kNormId));
            }
            if (node.PushNotificationPhoneStatusChange)
            {
                var phoneStatusChangeNotification = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS_PHONE);
                _notificationSubscriptionManager.Subscribe(userIdentifier, phoneStatusChangeNotification, new EntityIdentifier(typeof(KNorm), kNormId));
            } 
        }
         
        #region SetStatusAsync
        // [AbpAuthorize(PermissionNames.knorm_statuschange)]
        public async Task<KNormDto> SetStatusAsync([FromBody] KNormDto input)
        {
            var norm = await Repository.GetAsync(input.Id);
            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });


            if (input.NormStatus != NormStatus.Iptal)
                norm.TalepDurumu = await _kNormDetailAppService.GetNextStatu(input.Id);


            if (input.NormStatus == NormStatus.Iptal)
            {
                norm.NormStatus = NormStatus.Iptal;
                norm.TalepDurumu = TalepDurumu.RED_EDILDI_SONLANDI;
            }

            else if (!await _kNormDetailAppService.CheckStatus(input.Id))
            {
                norm.NormStatus = NormStatus.Onaylandi;
            }

            var result = await Repository.UpdateAsync(norm);
            await _notificationPublisherService.KNormStatusChanged(ObjectMapper.Map<KNormDto>(result), user);

            EventBus.Trigger(GetEventParameter(new EventHandlerEto<KNorm>
            {
                EventName = EventNames.KNORM_STATUS_CHANGED,
                Entity = result,
                LogType = ActivityLoggerTypes.ITEM_ADDED,
                DisplayKey = "Norm_Status_Changed"
            })); 
            return ObjectMapper.Map<KNormDto>(norm);

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
            eventParam.Url += eventParam.Id + "suratkargoulr";
            eventParam.UserId = _abpSession.GetUserId(); 
            return eventParam;
        }
        #endregion

        public async Task<KNormDto> GetByIdAsync(long id)
        {
            KNormDto dto = ObjectMapper.Map<KNormDto>(await base.GetEntityByIdAsync(id));
            long subeObjId = long.Parse(dto.SubeObjId);
            var sube = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = subeObjId });
            var bolge = await _kSubeAppService.GetAsync(new EntityDto<long> { Id = dto.BagliOlduguSubeObjId });


            if (!string.IsNullOrWhiteSpace(dto.PersonelId))
            {
                long personelId = long.Parse(dto.PersonelId);
                var personel = await _kPersonelAppService.GetById(personelId);
                dto.PersonelAdi = $"{personel.Ad} {personel.Soyad}";
            }

            dto.SubeAdi = sube.Adi;
            dto.BolgeAdi = bolge.Adi;


            return dto;
        }
    }
}

