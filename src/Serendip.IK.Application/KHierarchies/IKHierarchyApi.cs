using Refit;
using Serendip.IK.KHierarchies.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KHierarchies
{
    [Headers("Content-Type: application/json")]
    public interface IKHierarchyApi
    {
        [Get("/KKullanici/GetMail/{id}")]
        Task<KKullaniciDto> GetMail(long id);
    }
}