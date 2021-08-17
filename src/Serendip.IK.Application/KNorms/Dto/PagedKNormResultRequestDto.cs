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
                return _keyword.ToLower();
            }
            set
            {
                this._keyword = value != null ? value : "";
            }
        }
        public bool? IsActive { get; set; }
        public string Id { get; set; }
        public string BolgeId { get; set; }
        public string Type { get; set; }
    }
}
