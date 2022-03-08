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
        Task<int> GetKPromotionFilterByDepartmentCount(long departmentObjId);
        Task<List<IKPromotionFilterDto>> GetKPromotionFilterByUnit(long unitObjId);
        Task<int> GetKPromotionFilterByUnitCount(long unitObjId);
        Task<IKPromotionStatuDto> GetIKPromotionStatus();
        Task<IKPromotionTitleDto> GetIKPromotionTitles();
        Task<IKPromotionRequestTitleDto> GetIKPromotionRequestTitles(string title);
        Task<List<IKPromotionFilterDto>> GetKPromotionFilterData(PromotionUseFilterDto promotionUseFilterDto);
        Task<IKPromotionDto> GetIKPromotionHiearchyStatu(long id);
        
    }
}
