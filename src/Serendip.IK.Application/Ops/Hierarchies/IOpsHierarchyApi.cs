using Refit;
using Serendip.IK.Ops.Hierarchies.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies
{

    [Headers("Content-Type: application/json")]
    public interface IOpsHierarchyApi
    {

        [Get("/Kullanici/GetMail/{id}")]
        Task<OpsKullaniciDto> GetMail(long id);
    }
}
