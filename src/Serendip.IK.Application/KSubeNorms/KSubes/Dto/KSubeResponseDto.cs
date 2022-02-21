using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using SuratKargo.Core.Enums;

namespace Serendip.IK.KSubes.Dto
{
    [AutoMap(typeof(KSubeDto))]
    public class KSubeResponseDto : PagedResultRequestDto
    {
        [JsonProperty("objId")]
        public string ObjId { get; set; }
        [JsonProperty("adi")]
        public string Adi { get; set; }

        [JsonProperty("aktif")]
        public bool? Aktif { get; set; }
        [JsonProperty("tipi")]
        public KSubeTip? Tipi { get; set; }
        [JsonProperty("tipTur")]
        public KSubeTipTur? TipTur { get; set; }
    }
}
