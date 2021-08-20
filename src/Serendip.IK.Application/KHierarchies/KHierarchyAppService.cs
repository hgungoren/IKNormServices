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



        private readonly IUserAppService _userService;
        private readonly IAbpSession _session;
        private readonly IKSubeAppService _kSubeAppService;
        private readonly IKPersonelAppService _kPersonelAppService;


        public KHierarchyAppService(IRepository<KHierarchy, long> repository,
            IUserAppService userService,
            IAbpSession session,
            IKPersonelAppService kPersonelAppService,
            IKSubeAppService kSubeAppService) : base(repository)
        {

            _userService = userService;
            _session = session;
            _kSubeAppService = kSubeAppService;
            _kPersonelAppService = kPersonelAppService;
        }


        [DisableValidation]
        private async Task<List<KHierarchyDto>> GetKHierarcies(KHierarchyType type, string title)
        {
            var data = ObjectMapper.Map<List<KHierarchyDto>>(Repository.GetAllList(x => x.KHierarchyType == type).ToList().OrderBy(x => x.OrderNo));
            var user = data.Where(x => x.Title == title).FirstOrDefault();
            if (user != null)
            {
                return data.Where(x => x.OrderNo > user.OrderNo).OrderBy(x => x.OrderNo).ToList();
            }

            return data.OrderBy(x => x.OrderNo).ToList();
        }


        [AbpAuthorize(PermissionNames.khierarchy_view, PermissionNames.ksubedetail_norm_request_list)]
        public async Task<List<KHierarchyDto>> GetKHierarcies(string tip, string id)
        {
            var userId = _session.GetUserId();
            var user = await _userService.GetAsync(new EntityDto<long> { Id = userId });
            var sube = await _kSubeAppService.GetByCode(user.CompanyCode);

            List<KHierarchyDto> hierarchyDtos = new List<KHierarchyDto>();

            switch (tip)
            {
                case "None": 
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.None, user.Title);
                        break;
                    }
                case "Sube":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.Branch, user.Title);
                        break;
                    }
                case "Acente":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.Agent, user.Title);
                        break;
                    }
                case "Mikro":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.Micro, user.Title);
                        break;
                    }
                case "AktarmaMerkezi":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.Transfer, user.Title);
                        break;
                    }
                case "BolgeMudurlugu":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.Area, user.Title);
                        break;
                    }
                case "GeneralManager":
                case "Merkez":
                    {
                        hierarchyDtos = await GetKHierarcies(KHierarchyType.GeneralManager, user.Title);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
             
            var service = RestService.For<IKHierarchyApi>(SERENDIP_SERVICE_BASE_URL);
            var titles = hierarchyDtos.Select(x => x.Title).ToArray();
            var users = await _kPersonelAppService.GetKPersonelByBranchId(Convert.ToInt64(id), titles);
             
            var selectedUsers = users.Select(x => new KPersonelResponseDto
            {
                Ad = x.Ad,
                Soyad = x.Soyad,
                Email =    x.Kullanici_ObjId == 0 ? "": service.GetMail(x.Kullanici_ObjId).Result.Email,
                Gorevi = x.Gorevi  
            }).ToList();
              
            var hierarchies = hierarchyDtos.Select(x => new KHierarchyDto
            {
                Title = x.Title,
                FirstName = x.FirstName == null ? (selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault() != null ? selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault().Ad : null) : x.FirstName,
                LastName = x.LastName == null ? (selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault() != null ? selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault().Soyad : null) : x.LastName,
                Mail = x.Mail == null ? (selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault() != null ? selectedUsers.Where(u => u.Gorevi == x.Title).FirstOrDefault().Email : null) : x.Mail,
                OrderNo = x.OrderNo,
                GMYType = x.GMYType,
                NormalizedTitle = x.NormalizedTitle  
            }).ToList(); 

            return hierarchies;
        }
    }
}
