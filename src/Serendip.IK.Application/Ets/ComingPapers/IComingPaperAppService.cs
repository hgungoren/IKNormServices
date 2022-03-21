using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.Ets.ComingPapers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.ComingPapers
{
    public interface IComingPaperAppService : IAsyncCrudAppService<ComingPaperDto,long, PagedComingPaperResultRequestDto ,CreateComingPaperDto, ComingPaperDto> , ITransientDependency
    {
    }
}
