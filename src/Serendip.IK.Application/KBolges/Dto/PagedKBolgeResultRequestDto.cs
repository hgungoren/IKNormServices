using Abp.Application.Services.Dto;

namespace Serendip.IK.KBolges.Dto
{
    public class PagedKBolgeRequestDto : PagedAndSortedResultRequestDto
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
        public bool? IsActivity { get; set; }
        public int Tip { get; set; }
        public int Tur { get; set; }
    }
}
