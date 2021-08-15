using Abp.Application.Services.Dto;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    public class PagedKInkaLookUpTableResultRequestDto : PagedAndSortedResultRequestDto
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
    }
}
