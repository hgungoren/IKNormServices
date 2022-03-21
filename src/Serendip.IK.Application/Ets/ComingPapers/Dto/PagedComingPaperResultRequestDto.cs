using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.ComingPapers.Dto
{
    public class PagedComingPaperResultRequestDto : PagedAndSortedResultRequestDto
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
