using Abp.Application.Services.Dto;

namespace Serendip.IK.Users.Dto
{
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        private string _keyword = "";
        public string Keyword
        {
            get
            {
                return _keyword.ToString();
            }
            set
            {
                this._keyword = value != null ? value : "";
            }
        }
        public bool? IsActive { get; set; }
    }
}
