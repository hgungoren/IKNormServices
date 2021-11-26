using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Serendip.IK.Nodes.dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Nodes
{
    public class NodeAppService : IKCoreAppService<Node, NodeDto, long, PagedNodeRequestDto, NodeCreateInput, NodeUpdateInput>, INodeAppService
    {
        public NodeAppService(IRepository<Node, long> repository) : base(repository) { }

        public async Task<bool> UpdateStatuToPassive(ChangeStatuToPassiveDto dto)
        {
            var node = await Repository.FirstOrDefaultAsync(x => x.PositionId == dto.PositionId);

            if (node == null)
            {

                throw new UserFriendlyException("Ooppps! There is a problem!", "You are trying to see a product that is deleted...");
            }

            string[] names =
            { 
                "Mail",
                "MailStatusChange",
                "PushNotificationPhone",
                "PushNotificationPhoneStatusChange",
                "PushNotificationWeb",
                "PushNotificationWebStatusChange",
                "CanTerminate",
                "Active"
            };

            foreach (var item in node.GetType().GetProperties())
            {
                if (names.Contains(item.Name)) item.SetValue(node, false);
            }

            Repository.Update(node); 
            return node.Active;
        }

        public async Task<bool> UpdateStatus(ChangeStatusDto dto)
        {
            var node = await Repository.GetAsync(dto.Id);
            node.GetType().GetProperty(dto.Type).SetValue(node, dto.Status);
            Repository.Update(node);
            return dto.Status;
        }

        public async Task<bool> UpdateOrderNodes(int[] ids)
        { 
            if(ids.Length> 0)
            {
                var _node = Repository.FirstOrDefault(n  => n .Id == ids[0]);
                var nodes = Repository.GetAllList(n => n.PositionId == _node.PositionId);
                foreach (var n in nodes)
                {
                    n.OrderNo = 9999;

                    Repository.Update(n);
                }
            }
             
            for (int i = 0; i < ids.Length; i++)
            {
                var node = await Repository.GetAsync(ids[i]);
                node.OrderNo = i;
                await Repository.UpdateAsync(node);
            } 

            return true;
        }

        public async Task<bool> UpdateSetFalse(string id)
        {
            var nodes = await Repository.GetAllListAsync(x => x.PositionId == long.Parse(id));

            foreach (var node in nodes)
            {
                node.Selected = false; 
                Repository.Update(node);
            }

            return true;
        }

        public async Task<bool> UpdateSetTrue(ChangeSelectedTrueDto changeSelectedDto)
        {
            var nodes = await Repository.GetAllListAsync(x => changeSelectedDto.Ids.Contains(x.Id));

            foreach (var node in nodes)
            {
                node.Selected = true;
                Repository.Update(node);
            }
            return true;
        }

        public async Task<List<NodeDto>> GetNodesForKeys(PagedNodeResultRequestDto pagedNodeResultRequestDto)
        {
            var nodes = await Repository.GetAllListAsync(x => pagedNodeResultRequestDto.Keys.Contains(x.Id.ToString()));
            var _nodes = nodes.OrderBy(n => n.OrderNo);
            return ObjectMapper.Map<List<NodeDto>>(_nodes);
        }

        public async Task<List<NodeKeyValueDto>> GetNodesForKeyValues(PagedNodeResultRequestDto pagedNodeResultRequestDto)
        {
            var nodes = await Repository.GetAllListAsync(x => pagedNodeResultRequestDto.Keys.Contains(x.Id.ToString()));
            var _nodes = nodes.OrderBy(n => n.OrderNo); 
            return ObjectMapper.Map<List<NodeKeyValueDto>>(_nodes);
        }

        public async Task<List<NodeDto>> GetNodes(long PositionId)
        {
            var nodes = await Repository.GetAllListAsync(x => x.Selected == true && x.PositionId == PositionId);
            var _nodes = nodes.OrderBy(x => x.OrderNo); 
            return ObjectMapper.Map<List<NodeDto>>(_nodes);
        }
    }
}
