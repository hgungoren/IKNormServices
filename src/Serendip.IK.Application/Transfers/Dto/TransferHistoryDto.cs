using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Users.Dto;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Transfers.Dto
{
    [AutoMap(typeof(TransferHistory))]
    public class TransferHistoryDto : BaseEntityDto
    {

        public TransferHistoryDto()
        {
            this.Users = new List<UserDto>();
        }
        public long? FromUserId { get; set; }
        public long? FromUserGroupId { get; set; }
        public long ToUserId { get; set; }
        public long ToUserGroupId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public UserDto ToUser { get; set; }
        public UserDto FromUser { get; set; }
        public List<UserDto> Users { get; set; }
    }
}