using System;
using Abp.Application.Services.Dto;

namespace Serendip.IK.Emails.Dto
{
    public class GetAllEmailParemeter:PagedAndSortedResultRequestDto
    {
        public string ModelId { get; set; }
        public string ModelName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}