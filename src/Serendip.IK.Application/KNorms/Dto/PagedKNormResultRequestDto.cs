using Abp.Application.Services.Dto;
using System;
using System.Globalization;

namespace Serendip.IK.KNorms.Dto
{
    public class PagedKNormResultRequestDto : PagedAndSortedResultRequestDto
    {
        private string _keyword = "";
        private DateTime start;
        private DateTime end;

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
        public string Id { get; set; } = "0";
        public string BolgeId { get; set; }
        public string Type { get; set; }


        public DateTime? Start
        {
            get
            {
                try
                {
                    return start;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            set => start = value.HasValue ? value.Value : DateTime.Now;
        }
        public DateTime? End
        {
            get
            {
                try
                {
                   return end;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            set => end = value.HasValue ?  value.Value : DateTime.Now;
        }
    }
}
