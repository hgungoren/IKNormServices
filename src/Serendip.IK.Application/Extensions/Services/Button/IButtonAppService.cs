using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Serendip.IK.Extensions.Dto;

namespace Serendip.IK.Extensions.Services.Button
{
    public interface IButtonAppService : IApplicationService
    {
        //Task<List<ButtonDto>> GetAll(string area);

        //[Obsolete("This endpoint is Depricated. Instead of  this use Async overload version")]
        //Task<string> GetRedirectUrl(ButtonInvokeRequest request);

        //Task<string> GetRedirectUrlAsync(InvokeRequestDto request);
    }
}