using Refit;
using Serendip.IK.KBolges.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{
    [Headers("Content-Type: application/json")]
    public interface IKBolgeApi
    {
        [Get("/KSube/KBolge")]
        Task<IEnumerable<KBolgeDto>> GetAll();


        [Get("/KSube/GetById/{id}")]
        Task<KBolgeDto> Get([Query] long id);

        [Get("/KSube/GetBranchIds/{id}")]
        Task<List<long>> GetBranchIds(long id);
    }
}
