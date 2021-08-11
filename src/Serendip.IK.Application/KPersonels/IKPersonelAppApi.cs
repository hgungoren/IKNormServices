using Refit;
using Serendip.IK.KPersonels.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{
    [Headers("Content-Type: application/json")]
    public interface IKPersonelApi
    {
        [Get("/KPersonel")]
        Task<IEnumerable<KPersonelResponseDto>> GetAll();

        [Get("/KPersonel/{id}")]
        Task<IEnumerable<KPersonelResponseDto>> GetAllBySube(long id);

        [Get("/KPersonel/TotalEmployeeCount/{id}")]
        Task<int> TotalCount(long id);

        [Get("/KPersonel/TotalEmployeeCount")]
        Task<int> TotalCount();


        [Get("/KPersonel/GetKPersonelByBranchId/{id}/{title}")]
        Task<IEnumerable<KPersonelResponseDto>> GetKPersonelByBranchId(long id, [Body] string[] title);
    }
}
