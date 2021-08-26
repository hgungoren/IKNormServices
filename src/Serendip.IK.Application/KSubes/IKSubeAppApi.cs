using Refit;
using Serendip.IK.KSubes.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KSubes
{
    [Headers("Content-Type: application/json")]
    public interface IKSubeApi
    {
        [Get("/KSube/KSube")]
        Task<IEnumerable<KSubeDto>> GetAll(long id);

        [Get("/KSube/GetById/{id}")]
        Task<KSubeDto> Get([Query] long id);

        [Get("/KSube/GetBranchIds/{id}")]
        Task<List<long>> GetBranchIds(long id); 
        
    }
}
