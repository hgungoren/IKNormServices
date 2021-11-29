using Abp.Application.Services;
using Serendip.IK.Periods.Dto;
using System;
using System.Threading.Tasks;

namespace Serendip.IK.Periods
{
    public interface IPeriodAppService : IAsyncCrudAppService<PeriodDto, long>
    {
        Task<PeriodDto> GetById(long id);
        Task<PeriodDto> GetByDate(DateTime date);
    }
}
