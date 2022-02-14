using Abp.Application.Services;
using Serendip.IK.Ops.Nodes.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Nodes
{
    public interface IOpsNodeAppService : IAsyncCrudAppService<OpsNodeDto, long, OpsPagedNodeRequestDto, OpsNodeCreateInput, OpsNodeUpdateInput>
    {

        /// <summary>
        /// Tek Bir Node Değerini Değiştirir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateStatus(OpsChangeStatusDto dto);

        /// <summary>
        /// Verilen Pozisyon Id Değerine Göre, Tüm Alanları Pasif Eder
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateStatuToPassive(OpsChangeStatuToPassiveDto dto); 
        Task<bool> UpdateOrderNodes(int[] ids); 
        Task<bool> UpdateSetFalse(string id);
        Task<bool> UpdateSetTrue(OpsChangeSelectedTrueDto changeSelectedDto);


        Task<int> GetOpsNodesCode();

    }
}
