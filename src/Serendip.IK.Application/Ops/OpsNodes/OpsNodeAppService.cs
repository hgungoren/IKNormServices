using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.Nodes.dto;
using Serendip.IK.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Nodes
{
    public class OpsNodeAppService : IKCoreAppService<OpsNode, OpsNodeDto, long, OpsPagedNodeRequestDto, OpsNodeCreateInput, OpsNodeUpdateInput>
    {

        private readonly IAbpSession _abpSession;
        private IUserAppService _userAppService;



        #region Constructor
        public OpsNodeAppService(
           IRepository<OpsNode, long> repository , IAbpSession abpSession, IUserAppService userAppService
           ) : base(repository) {

            _abpSession = abpSession;
            _userAppService = userAppService;

        }
        #endregion

        #region UpdateStatuToPassive
        public async Task<bool> UpdateStatuToPassive(OpsChangeStatuToPassiveDto dto)
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
        #endregion

        #region UpdateStatus
        public async Task<bool> UpdateStatus(OpsChangeStatusDto dto)
        {
            var node = await Repository.GetAsync(dto.Id);
            node.GetType().GetProperty(dto.Type).SetValue(node, dto.Status);
            Repository.Update(node);

            return dto.Status;
        }
        #endregion

        #region UpdateOrderNodes
        public async Task<bool> UpdateOrderNodes(int[] ids)
        {
            if (ids.Length > 0)
            {
                var _node = Repository.FirstOrDefault(n => n.Id == ids[0]);
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
        #endregion

        #region UpdateSetFalse
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
        #endregion

        #region UpdateSetTrue
        public async Task<bool> UpdateSetTrue(OpsChangeSelectedTrueDto changeSelectedDto)
        {
            var nodes = await Repository.GetAllListAsync(x => changeSelectedDto.Ids.Contains(x.Id));

            foreach (var node in nodes)
            {
                node.Selected = true;
                Repository.Update(node);
            }

            return true;
        }
        #endregion

        #region GetNodesForKeys
        public async Task<List<OpsNodeDto>> GetNodesForKeys(OpsPagedNodeResultRequestDto pagedNodeResultRequestDto)
        {
            var nodes = await Repository.GetAllListAsync(x => pagedNodeResultRequestDto.Keys.Contains(x.Id.ToString()));
            var _nodes = nodes.OrderBy(n => n.OrderNo);

            return ObjectMapper.Map<List<OpsNodeDto>>(_nodes);
        }
        #endregion

        #region GetNodesForKeyValues
        public async Task<List<OpsNodeKeyValueDto>> GetNodesForKeyValues(OpsPagedNodeResultRequestDto pagedNodeResultRequestDto)
        {
            var nodes = await Repository.GetAllListAsync(x => pagedNodeResultRequestDto.Keys.Contains(x.Id.ToString()));
            var _nodes = nodes.OrderBy(n => n.OrderNo);
            
            return ObjectMapper.Map<List<OpsNodeKeyValueDto>>(_nodes);
        }
        #endregion

        #region GetNodes
        public async Task<List<OpsNodeDto>> GetNodes(long PositionId)
        {
            var nodes = await Repository.GetAllListAsync(x => x.Selected == true && x.PositionId == PositionId);
            var _nodes = nodes.OrderBy(x => x.OrderNo);

            return ObjectMapper.Map<List<OpsNodeDto>>(_nodes);
        } 



        public async Task<bool> GetCompensationStatusCheck()
        {
            long PositionId = 1;
            var nodes = await Repository.GetAllListAsync(x => x.Selected == true && x.PositionId == PositionId);
            var substitle = nodes.Where(x => x.Selected == true).ToList().OrderByDescending(x => x.OrderNo);
            string substitleText="";
            foreach (var item in substitle)
            {
                substitleText = item.SubTitle;
                break;
            }
            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            // "DAMAGECOMPENSATIONEXPERT
            var getRolesList = await _userAppService.GetRoles();      
            foreach (var item in user.RoleNames)
            {
                if (substitleText == item)
                {
                    return true;
                }
            }

            return false;
        }
  
        #endregion
    }
}
