using Abp.Application.Services;
using Serendip.IK.Transfers.Dto;

namespace Serendip.IK.Transfers
{
    public interface ITransferHistoryAppService : IAsyncCrudAppService<TransferHistoryDto, long>
    {
        //Task Transfer(TransferHistoryDto dto, bool authUser = true, bool authUserGroup = true);

        //void CreateFirstHistory(TransferHistoryDto dto);
        //Task<PagedResultDto<TransferHistoryDto>> GetAllHistories(long id, TransferHistoryFilterDto filter);
    }
}