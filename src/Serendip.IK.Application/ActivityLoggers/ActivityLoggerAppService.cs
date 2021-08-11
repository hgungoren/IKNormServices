using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.ActivityLoggers.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.ActivityLoggers
{
    public class ActivityLoggerAppService : CoreAppServiceBase, IActivityLoggerAppService
    {
        private IRepository<ActivityLogger, long> _repository;
         
        public ActivityLoggerAppService(IRepository<ActivityLogger, long> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultDto<ActivityLoggerDto>> GetLogsAsync(long entityId, string entityType, PagedAndSortedResultRequestDto filter)
        {
            var query = _repository.GetAll().Where(x => x.ModelName == entityType && x.ModelId == entityId.ToString()).OrderByDescending(x => x.Date);
            int count = await query.CountAsync();
            return new PagedResultDto<ActivityLoggerDto>
            {
                TotalCount = count,
                Items = ObjectMapper.Map<List<ActivityLoggerDto>>(await query.Skip(filter.SkipCount).Take(filter.MaxResultCount).ToListAsync())
            };
        }

        public List<ActivityLogViewDto> GetRecents(RecentRequestParam param)
        {
            var recents = _repository.GetAll().Where(x => x.Name == param.Type && (x.ModelName == ModelTypes.CUSTOMER || x.ModelName == ModelTypes.CONTACT || x.ModelName == ModelTypes.OPPORTUNITY)).OrderByDescending(x => x.Date).Take(param.TakeCount).ToList();

            var result = new List<ActivityLogViewDto>();

            foreach (var item in recents)
            {
                //if (item.ModelName == ModelTypes.CUSTOMER)
                //{
                //    var model = _customerRepository.FirstOrDefault(x => x.Id == Guid.Parse(item.ModelId))
                //        ?? throw new EntityNotFoundException(L("DataNotFound"), instance: item.ModelId, new NullReferenceException(nameof(Account)));
                //    if (model != null)
                //    {
                //        result.Add(new ActivityLogViewDto
                //        {
                //            DisplayName = model.Name,
                //            Path = UrlGenerator.Url("customer_view", new { Id = model.Id }),
                //            Type = ModelTypes.CUSTOMER,
                //            Id = model.Id.ToString()
                //        });
                //    }

                //}
                //else if (item.ModelName == ModelTypes.CONTACT)
                //{
                //    var model = _contactRepository.FirstOrDefault(x => x.Id == Guid.Parse(item.ModelId))
                //        ?? throw new EntityNotFoundException(L("DataNotFound"), instance: item.ModelId.ToString(), new NullReferenceException(nameof(Contact)));
                //    if (model != null)
                //    {
                //        result.Add(new ActivityLogViewDto
                //        {
                //            DisplayName = model.Name,
                //            Path = UrlGenerator.Url("contact_view", new { Id = model.Id }),
                //            Type = ModelTypes.CONTACT,
                //            Id = model.Id.ToString()
                //        });
                //    }
                //}
                //else if (item.ModelName == ModelTypes.OPPORTUNITY)
                //{
                //    var model = _opportunityRepository.FirstOrDefault(x => x.Id == Guid.Parse(item.ModelId))
                //        ?? throw new EntityNotFoundException(L("DataNotFound"), instance: item.ModelId, new NullReferenceException(nameof(Opportunity)));
                //    if (model != null)
                //    {
                //        result.Add(new ActivityLogViewDto
                //        {
                //            DisplayName = model.Title,
                //            Path = UrlGenerator.Url("opportunity_view", new { Id = model.Id }),
                //            Type = ModelTypes.OPPORTUNITY,
                //            Id = model.Id.ToString()
                //        });
                //    }
                //}
            }

            return result;
        }

        //[Abp.Domain.Uow.UnitOfWork]
        //public async void SaveLog(ActivityLoggerDto log)
        //{
        //    var entity = ObjectMapper.Map<ActivityLogger>(log);
        //    await _repository.InsertAsync(entity);
        //}
    }
}
