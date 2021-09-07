using Abp.Domain.Repositories;
using Abp.UI;
using Serendip.IK.Nodes.dto;
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
                 "Mail","MailStatusChange","PushNotificationPhone","PushNotificationPhoneStatusChange","PushNotificationWeb","PushNotificationWebStatusChange","CanTerminate","Active"
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
    }


}
