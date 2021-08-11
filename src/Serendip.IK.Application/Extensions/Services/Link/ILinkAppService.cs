using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Serendip.IK.Extensions.Dto;

namespace Serendip.IK.Extensions.Services.Link
{
    public interface ILinkAppService : IApplicationService
    {
        //Task<List<LinkDto>> GetAll(string area);

        //[Obsolete("This endpoint is Depricated. Instead of  this use Async overload version")]
        //Task<string> GetRedirectUrl(LinkInvokeRequest request);

        //Task<string> GetRedirectUrlAsync(InvokeRequestDto request);

    }
}