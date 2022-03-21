using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Serendip.IK.Ets.ComingPapers.Dto;
using Serendip.IK.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.ComingPapers
{
    public class ComingPaperAppService : AsyncCrudAppService< ComingPaper, ComingPaperDto, long, PagedComingPaperResultRequestDto, CreateComingPaperDto, ComingPaperDto> , IComingPaperAppService
    {
        private IUserAppService _userAppService;
        private readonly IAbpSession _abpSession;
        

        

        public ComingPaperAppService(IRepository<ComingPaper, long> repository, IUserAppService userAppService, IAbpSession abpSession) : base(repository)
        {
            _userAppService = userAppService;
            _abpSession = abpSession;
        }
    }
}
