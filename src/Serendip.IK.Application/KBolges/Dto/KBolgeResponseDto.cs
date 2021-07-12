using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using SuratKargo.Core.Enums;

namespace Serendip.IK.KBolges.Dto
{
    [AutoMap(typeof(KBolgeDto))]
    public class KBolgeResponseDto : PagedResultRequestDto
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
