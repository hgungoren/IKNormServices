using Abp.Application.Services;
using Serendip.IK.IKPromotions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.IKPromotions
{
    public interface IIKPromotionAppService : IAsyncCrudAppService<IKPromotionDto, long, PagedIKPromotionResultRequestDto, CreateIKPromotionDto, IKPromotionDto>
    {
        Task<bool> IsAnyPersonel(string registirationNumber);
        Task<List<IKPromotionFilterDto>> GetKPromotionFilterByDepartment(long departmentObjId);
        Task<List<IKPromotionFilterDto>> GetKPromotionFilterByUnit(long unitObjId);
    }
}
