using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Serendip.IK
{
    public class IKCoreAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> :
        AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public IKCoreAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {

        }
    }
}
