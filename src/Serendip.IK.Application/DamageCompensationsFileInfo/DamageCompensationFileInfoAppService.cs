using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Serendip.IK.DamageCompensationsFileInfo
{
    public class DamageCompensationFileInfoAppService : AsyncCrudAppService<
        DamageCompensationFileInfo,
        DamageCompensationFileInfoDto,
        long,
        PagedDamageCompensationFileInfoResultRequestDto,
        CreateDamageCompensationFileInfoDto,
        DamageCompensationFileInfoDto>,IDamageCompensationFileInfoAppService
    {


        #region Constructor


        private IUserAppService _userAppService;


        #endregion




        public DamageCompensationFileInfoAppService(IRepository<DamageCompensationFileInfo, long> repository, IUserAppService userAppService) : base(repository)
        {
            _userAppService = userAppService;

        }

        public override Task<DamageCompensationFileInfoDto> CreateAsync(CreateDamageCompensationFileInfoDto input)
        {
            return base.CreateAsync(input);
        }


    

    }
}
