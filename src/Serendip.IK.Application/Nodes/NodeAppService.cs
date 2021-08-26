using Abp.Domain.Repositories;
using Serendip.IK.Nodes.dto;
using System.Threading.Tasks;

namespace Serendip.IK.Nodes
{


    public class NodeAppService : IKCoreAppService<Node, NodeDto, long, PagedNodeRequestDto, NodeCreateInput, NodeUpdateInput>, INodeAppService
    {
        public NodeAppService(IRepository<Node, long> repository) : base(repository) { }




        public async Task<bool> UpdateStatus(ChangeStatusDto dto)
        { 
            var node = await  Repository.GetAsync(dto.Id);
            node.GetType().GetProperty(dto.Type).SetValue(node, dto.Status);
            Repository.Update(node); 
            return dto.Status;
        } 
    }


}
