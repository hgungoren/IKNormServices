using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Ops.dto;
using Serendip.IK.Ops.Positions.Dto;
using Serendip.IK.Ops.Units.dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Units
{
    public class OpsUnitAppService : IKCoreAppService<OpsUnit, OpsUnitDto, long, OpsPagedUnitRequestDto, OpsUnitCreateInput, OpsUnitUpdateInput>, IOpsUnitAppService
    {
        #region Constructor
        public OpsUnitAppService(IRepository<OpsUnit, long> repository) : base(repository) { }
        #endregion

        #region CreateAsync
        public override Task<OpsUnitDto> CreateAsync(OpsUnitCreateInput input)
        {
            return base.CreateAsync(input);
        }
        #endregion

        #region GetByUnit
        public async Task<OpsUnitDto> GetByUnit(string unit)
        {
            var result = await Repository.GetAll()
                .Where(u => u.Code == unit)
                .Include(x => x.Positions)
                .ThenInclude(x => x.Nodes)
                .Select(x => new OpsUnitDto
                {
                    Code = x.Code,
                    Name = x.Name,
                    Id = x.Id,
                    Positions = x.Positions.Select(p => new OpsPositionDto
                    {
                        Name = p.Name,
                        Code = p.Name,
                        UnitId = x.Id,
                        Id = p.Id,
                        Nodes = p.Nodes.Select(n => new Nodes.Dto.OpsNodeDto
                        {
                            Id = n.Id,
                            Title = n.Title,
                            Code = n.Code,
                            SubTitle = n.SubTitle,
                            Expanded = n.Expanded,
                            OrderNo = n.OrderNo,
                            PositionId = n.PositionId,
                            Mail = n.Mail,
                            PushNotificationPhone = n.PushNotificationPhone,
                            PushNotificationWeb = n.PushNotificationWeb,
                            MailStatusChange = n.MailStatusChange,
                            Active = n.Active,
                            CanTerminate = n.CanTerminate
                        }).OrderBy(n => n.OrderNo)
                    })
                }).FirstOrDefaultAsync();

            return result;
          
        }
        #endregion

        #region CreateFilteredQuery
        protected override IQueryable<OpsUnit> CreateFilteredQuery(OpsPagedUnitRequestDto input)
        {
            var data = base.CreateFilteredQuery(input).Include(x => x.Positions).ThenInclude(x => x.Nodes.OrderBy(x => x.OrderNo));
            return data;
           
        } 
        #endregion
    }
}
