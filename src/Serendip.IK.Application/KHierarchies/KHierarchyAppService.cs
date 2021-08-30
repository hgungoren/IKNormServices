using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KHierarchies.Dto;
using Serendip.IK.KPersonels;
using Serendip.IK.KPersonels.Dto;
using Serendip.IK.KSubes;
using Serendip.IK.Units;
using Serendip.IK.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KHierarchies
{

    public class KHierarchyAppService : AsyncCrudAppService<KHierarchy, KHierarchyDto, long, PagedKHierarchyResultRequestDto, CreateKHierarchyDto, KHierarchyDto>, IKHierarchyAppService
    {
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_KULLANICI_API_URL;

        #region Constructor
        private readonly IUserAppService _userService;
        private readonly IAbpSession _session;
        private readonly IKSubeAppService _kSubeAppService;
        private readonly IKPersonelAppService _kPersonelAppService;
        private readonly IUnitAppService _unitAppService;

        public KHierarchyAppService(IRepository<KHierarchy, long> repository,
            IUserAppService userService,
            IAbpSession session,
            IKPersonelAppService kPersonelAppService,
            IUnitAppService unitAppService,
            IKSubeAppService kSubeAppService) : base(repository)
        {

            _userService = userService;
            _session = session;
            _kSubeAppService = kSubeAppService;
            _kPersonelAppService = kPersonelAppService;
            _unitAppService = unitAppService;
        }
        #endregion

        public async Task<List<KHierarchyDto>> GetHierarchy(GenerateHierarchyDto dto)
        {
            var hierarchy = _unitAppService.GetByUnit(dto.Tip, dto.Pozisyon);
            var position = hierarchy.Positions.FirstOrDefault();
            var titles = position.Nodes.Where(x => x.Active).Select(n => n.Title).ToArray();
            var users = await _kPersonelAppService.GetKPersonelByEmails(titles);

            List<KHierarchyDto> Hierarcies = new List<KHierarchyDto>();
            foreach (string title in titles)
            {
                var node = position.Nodes.FirstOrDefault(x => x.Title == title); 
                KPersonelDto user;
                user = title switch
                {
                    "Operasyon Genel Müdür Yrd." => new KPersonelDto
                    { 
                        ObjId = "3120000100000430125",
                        Ad = "Tolga",
                        Soyad = "Karaduman",
                        alan5 = "tolga.karaduman@suratkargo.com.tr"
                    },
                    "İnsan Kaynakları Genel Müdür Yrd." => new KPersonelDto
                    {
                        ObjId = "3120000100000430409",
                        Ad = "Engin",
                        Soyad = "Aktepe",
                        alan5 = "engin.aktepe@suratkargo.com.tr"
                    },
                    _ => users.Where(x => x.Gorevi == title).Count() > 1 ?
                     users.Where(x => x.IsYeri_ObjId == dto.BolgeId.ToString() && x.Gorevi == title).FirstOrDefault() :
                     users.Where(x => x.Gorevi == title).FirstOrDefault()
                };

                if (user == null) continue;

                KHierarchyDto kHierarchyDto = new KHierarchyDto
                {
                    Title = title,
                    KHierarchyType = default,
                    OrderNo = node.OrderNo,
                    MasterId = default,
                    EndApprove = default,
                    Mail = user.alan5,
                    FirstName = user.Ad,
                    LastName = user.Soyad,
                    GMYType = default,
                    NormalizedTitle = default,
                    ObjId = user.ObjId
                };
                Hierarcies.Add(kHierarchyDto);
            }

            return Hierarcies;
        } 
    }
}
