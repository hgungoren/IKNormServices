using Abp.Application.Services;
using Serendip.IK.Nodes.dto;
using System.Threading.Tasks;

namespace Serendip.IK.Nodes
{
    public interface INodeAppService : IAsyncCrudAppService<NodeDto, long, PagedNodeRequestDto, NodeCreateInput, NodeUpdateInput>
    {

        /// <summary>
        /// Tek Bir Node Değerini Değiştirir
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateStatus(ChangeStatusDto dto);


        /// <summary>
        /// Verilen Pozisyon Id Değerine Göre, Tüm Alanları Pasif Eder
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateStatuToPassive(ChangeStatuToPassiveDto dto);


        Task<bool> UpdateOrderNodes(int[] ids);


    }


}
