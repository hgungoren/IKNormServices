using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.KPersonels;
using Serendip.IK.KPersonels.Dto;
using Serendip.IK.Ops.Hierarchy;
using Serendip.IK.Units;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies.Dto
{
    public class OpsHierarchyAppService : AsyncCrudAppService<OpsHierarchy, OpsHierarchyDto, long, OpsPagedHierarchyResultRequestDto, OpsCreateHierarchyDto, OpsHierarchyDto>, IOpsHierarchyAppService
    {

        #region Constructor 
        private readonly IKPersonelAppService _kPersonelAppService;
        private readonly IUnitAppService _unitAppService;

        public OpsHierarchyAppService(
            IRepository<OpsHierarchy, long> repository,
            IKPersonelAppService kPersonelAppService,
            IUnitAppService unitAppService
            ) : base(repository)
        {
            this._kPersonelAppService = kPersonelAppService;
            this._unitAppService = unitAppService;
        }
        #endregion

        #region GetHierarchy
        //[AbpAuthorize(PermissionNames.items_hierarchy_view)]
        public async Task<List<OpsHierarchyDto>> GetHierarchy(OpsGenerateHierarchyDto dto)
        {
            var hierarchy = await _unitAppService.GetByUnit(dto.Tip);
            var position = hierarchy.Positions.Where(x => x.Name.Trim() == dto.Pozisyon).FirstOrDefault();

            if (position == null)
            {
                return null;
            }

            var titles = position.Nodes.Where(x =>
            x.PushNotificationPhoneStatusChange ||
            x.PushNotificationPhone ||
            x.PushNotificationWeb ||
            x.PushNotificationWebStatusChange ||
            x.Mail ||
            x.MailStatusChange

            ).Select(n => n.Title).ToArray();
            var users = await _kPersonelAppService.GetKPersonelByEmails(titles);

            List<OpsHierarchyDto> Hierarcies = new List<OpsHierarchyDto>();
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

                OpsHierarchyDto kHierarchyDto = new OpsHierarchyDto
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
        #endregion

    }
}
