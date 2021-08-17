using Abp.Application.Services.Dto;

namespace Serendip.IK.KPersonels.Dto
{
    public class PagedKPersonelResultRequestDto : PagedResultRequestDto
    {
        private string _keyword = "";
        public string Keyword
        {
            get
            {
                return _keyword.ToLower();
            }
            set
            {
                this._keyword = value != null ? value : "";
            }
        }
        public bool? IsActive { get; set; }
        public string Id { get; set; }
        public bool? IsActivity { get; set; }
    }
}
