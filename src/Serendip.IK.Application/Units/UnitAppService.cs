using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Units.dto;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Units
{
    public class UnitAppService : IKCoreAppService<Unit, UnitDto, long, PagedUnitRequestDto, UnitCreateInput, UnitUpdateInput>, IUnitAppService
    {
        public UnitAppService(IRepository<Unit, long> repository) : base(repository) { }

        public override Task<UnitDto> CreateAsync(UnitCreateInput input)
        {
            try
            {
                return base.CreateAsync(input);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }


        protected   override IQueryable<Unit> CreateFilteredQuery(PagedUnitRequestDto input)
        {
            try
            {
                var data = base.CreateFilteredQuery(input).Include(x => x.Positions).ThenInclude(x => x.Nodes);
                return data;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        } 
    }
}
