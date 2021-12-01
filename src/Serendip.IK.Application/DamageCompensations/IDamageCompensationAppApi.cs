using Refit;
using Serendip.IK.DamageCompensations.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations
{
    [Headers("Content-Type: application/json")]
    public interface IDamageCompensationApi
    { 
        [Get("/KKargo/GetDamage/{takipNo}")]
        Task<DamageCompensationDto> GetDamageCompensations(long takipNo);
         
        [Get("/KCari/GetCariListAsynDamage/{id}")]
        Task<List<DamageCompensationGetCariListDto>> GetCariListAsynDamage(string id);
         
        [Get("/KSube/GetKSubeListDamageAll")]
        Task<List<DamageCompensationGetBranchsListDto>> GetKSubeListDamageAll();
         
        [Get("/KSube/GetKBolgeListDamageAll")]
        Task<List<DamageCompensationGetBranchsListDto>> GetKBolgeListDamageAll();

        [Get("/KBirim/GetAllAsync")]
        Task<List<DamageCompensationGetBirimListDto>> GetAllAsync();

        Task<int> GetDamageLastId();

        Task<List<GetDamageCompensationAllList>> GetAllDamageCompensation();
    }
}
