using Abp.Application.Services;
using Serendip.IK.Nodes.dto;
using System.Threading.Tasks;

namespace Serendip.IK.Nodes
{
    public interface INodeAppService : IAsyncCrudAppService<NodeDto, long, PagedNodeRequestDto, NodeCreateInput, NodeUpdateInput> {
        Task<bool> UpdateStatus(ChangeStatusDto dto);
    }

     
}
