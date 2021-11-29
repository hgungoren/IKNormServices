using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Periods.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Periods
{
    public class PeriodAppService : CoreAsyncCrudAppService<Period, PeriodDto, long>, IPeriodAppService
    {
        #region Constructor
        //IPeriodRepository _periodRepository;

        public PeriodAppService(
            IRepository<Period, long> repository
            ) : base(repository)
        {
            //_periodRepository = repository;
        }
        #endregion

        #region GetById
        public async Task<PeriodDto> GetById(long id)
        {
            return await Repository.GetAll()
                .Select(x => MapToEntityDto(x))
                .FirstOrDefaultAsync(x => x.Id == id); //   ?? throw new EntityNotFoundException(nameof(Period) + L("DataNotFound"), instance: id.ToString());
        }
        #endregion

        #region GetByDate
        public async Task<PeriodDto> GetByDate(DateTime date)
        {
            return await Repository.GetAll()
                .Where(x => x.StartDate <= date && x.EndDate >= date)
                .Select(x => MapToEntityDto(x))
                .FirstOrDefaultAsync();
        }
        #endregion

        #region CreateAsync
        public override async Task<PeriodDto> CreateAsync(PeriodDto input)
        {
            var result = await base.CreateAsync(input);

            SaveLog(ActivityLoggerTypes.ITEM_ADDED, "Log_Period_Added", modelName: ModelTypes.PERIOD, modelId: result.Id.ToString());
            return result;
        }
        #endregion

        #region UpdateAsync
        public override async Task<PeriodDto> UpdateAsync(PeriodDto input)
        {
            var result = await base.UpdateAsync(input);

            SaveLog(ActivityLoggerTypes.ITEM_UPDATED, "Log_Period_Updated", modelName: ModelTypes.PERIOD, modelId: result.Id.ToString());
            return result;
        }
        #endregion

        #region DeleteAsync
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var entity = await GetById(input.Id);
            await base.DeleteAsync(entity);

            SaveLog(ActivityLoggerTypes.ITEM_REMOVED, "Log_Period_Removed", modelName: ModelTypes.PERIOD, modelId: entity.Id.ToString());
        } 
        #endregion
    }
}
