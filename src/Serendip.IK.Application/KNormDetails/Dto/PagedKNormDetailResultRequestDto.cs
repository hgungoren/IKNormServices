using Abp.Application.Services.Dto;

namespace Serendip.IK.KNormDetails.Dto
{
    public class PagedKNormDetailResultRequestDto : PagedAndSortedResultRequestDto
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
        public long Id { get; set; }
    }
}
