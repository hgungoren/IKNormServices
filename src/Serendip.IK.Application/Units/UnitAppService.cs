﻿using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Positions.dto;
using Serendip.IK.Units.dto;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Units
{
    public class UnitAppService : IKCoreAppService<Unit, UnitDto, long, PagedUnitRequestDto, UnitCreateInput, UnitUpdateInput>, IUnitAppService
    {
        public UnitAppService(IRepository<Unit, long> repository) : base(repository) { }

        public override Task<UnitDto> CreateAsync(UnitCreateInput input)
        {
            try
            {
                return base.CreateAsync(input);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }


        public UnitDto GetByUnit(string unit, string positions)
        {
            try
            {
                var result = Repository.GetAll()
                    .Where(u => u.Code == unit)
                    .Include(x => x.Positions.Where(x => x.Name == positions))
                    .ThenInclude(x => x.Nodes)
                    .Select(x => new UnitDto
                    {
                        Code = x.Code,
                        Name = x.Name,
                        Id = x.Id,
                        Positions = x.Positions.Select(p => new PositionDto
                        {
                            Name = p.Name,
                            Code = p.Name,
                            UnitId = x.Id,
                            Id = p.Id,
                            Nodes = p.Nodes.Select(n => new Nodes.dto.NodeDto
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
                            })
                        })
                    })

                    .FirstOrDefault();

                return result;
            }
            catch (System.Exception e)
            {

                throw;
            }

        }

        protected override IQueryable<Unit> CreateFilteredQuery(PagedUnitRequestDto input)
        {
            try
            {
                var data = base.CreateFilteredQuery(input).Include(x => x.Positions).ThenInclude(x => x.Nodes);
                return data;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}