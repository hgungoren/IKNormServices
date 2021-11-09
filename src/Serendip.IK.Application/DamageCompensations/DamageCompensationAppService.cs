using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Refit;
using Serendip.IK.DamageCompensations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations
{
    public class DamageCompensationAppService : AsyncCrudAppService<DamageCompensation, DamageCompensationDto, long, PagedDamageCompensationResultRequestDto, CreateDamageCompensationDto, DamageCompensationDto>, IDamageCompensationAppService
    {

        #region Constructor
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_KKARGO_API_URL;
        private const string SERENDIP_K_KCARI_API_URL = ApiConsts.K_KCARI_API_URL;
        #endregion


        public DamageCompensationAppService(IRepository<DamageCompensation, long> repository) : base(repository)
        {

        }

        public override Task<DamageCompensationDto> CreateAsync(CreateDamageCompensationDto input)
        {
            return base.CreateAsync(input);
        }

        public async Task<DamageCompensationDto> GetById(long id)
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_SERVICE_BASE_URL);         
                var data = await service.GetDamageCompensations(id);
                return data;
           
           
        }

        public  async Task<List<DamageCompensationGetCariListDto>> GetCariListAsynDamage(string id)
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KCARI_API_URL);
            var data = await service.GetCariListAsynDamage(id);
            return data;



        }












    }
}
