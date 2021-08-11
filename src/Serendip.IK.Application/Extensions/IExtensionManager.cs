using Abp.Application.Services;
using Serendip.IK.Extensions.Core;
using Serendip.IK.Extensions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions
{
    public interface IExtensionManager : IApplicationService
    {
        Task<ExtensionInvocationResult> Invoke(string name, object data);

        //Task<ExtensionInvocationResult> Invoke(long id, object data );

        Task<List<IExtensionItem>> GetItems(string name);

        Task<ExtensionDefination> Install(string data);

        bool IsInstalled(string name);

        Task<List<ExtensionDto>> GetExtensions();

        Task<List<MarketplaceItemDto>> GetMarketplace();
        Task<MarketplaceItemDto> GetMarketplaceItem(long id);

        Task<ExtensionDefination> InstallFromMarketplace(long marketplaceId);

        Task<ExtensionDto> GetExtension(long id);

        Task<ExtensionDefination> GetExtensionDefination(long id);

        Task SetConfiguration(ExtensionConfigurationParameter parameter);

        Task<ExtensionDto> ChangeStatus(long id, ExtensionStatus status);
        Task Uninstall(long id);
    }
}