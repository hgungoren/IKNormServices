using Refit;
using Serendip.IK.KInkaLookUpTables.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KInkaLookUpTables
{
    [Headers("Content-Type: application/json")]
    public interface IKInkaLookUpTableApi
    { 
        [Get("/IKLookUpTable/{key}")]
        Task<IEnumerable<KInkaLookUpTableDto>> GetTableAsync([Query] string key);  
    }
}
