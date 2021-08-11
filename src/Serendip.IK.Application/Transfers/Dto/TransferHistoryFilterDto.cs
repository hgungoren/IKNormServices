using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Serendip.IK.Transfers.Dto
{
    [AutoMap(typeof(TransferHistory))]
    public class TransferHistoryFilterDto : PagedAndSortedResultRequestDto
    {
    }
}
