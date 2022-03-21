using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.Authorization.Users;

namespace Serendip.IK.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public string CompanyObjId { get; set; }
        public string CompanyRelationObjId { get; set; }
        public string TcKimlikNo { get; set; }
    }
}
