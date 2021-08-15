using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Serendip.IK.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string CompanyCode { get; set; }


        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public string NormalizedTitle { get; set; }


         
        public int SicilNo { get; set; }
        public long UserObjId { get; set; } 
        public long CompanyObjId { get; set; }
        public long CompanyRelationObjId { get; set; }


    }
}
