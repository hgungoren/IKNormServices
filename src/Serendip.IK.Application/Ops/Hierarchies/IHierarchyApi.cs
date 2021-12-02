using Refit;
using Serendip.IK.Ops.Hierarchies.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies
{

    [Headers("Content-Type: application/json")]
    public interface IHierarchyApi
    {

        [Get("/Kullanici/GetMail/{id}")]
        Task<KullaniciDto> GetMail(long id);
    }
}
