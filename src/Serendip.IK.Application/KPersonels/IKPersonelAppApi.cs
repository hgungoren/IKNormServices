using Microsoft.AspNetCore.Mvc;
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
        Task<IEnumerable<KPersonelResponseDto>> GetAllBySube(string id);

        [Get("/KPersonel/TotalEmployeeCount/{id}")]
        Task<int> TotalCount(long id);

        [Get("/KPersonel/TotalEmployeeCount")]
        Task<int> TotalCount();

        [Get("/KPersonel/GetKPersonelByBranchId/{id}/{title}")]
        Task<IEnumerable<KPersonelResponseDto>> GetKPersonelByBranchId(long id, [Body] string[] title);



        [Get("/KPersonel/GetKPersonelByEmail/{email}")]
        Task<KPersonelInfoDto> GetKPersonelByEmail(string email);


        [Get("/KPersonel/GetKPersonelInfo/{id}")]
        Task<KPersonelDto> GetKPersonelById(long id);


        [Get("/KPersonel/GetKPersonelByEmails")]
        Task<List<KPersonelDto>> GetKPersonelByEmails([Body] string[] email);

        
    }
}
