using Abp.Application.Services.Dto;

namespace Serendip.IK.KNorms.Dto
{
    public class PagedKNormResultRequestDto : PagedAndSortedResultRequestDto
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
        public long BolgeId { get; set; }
    }
}
