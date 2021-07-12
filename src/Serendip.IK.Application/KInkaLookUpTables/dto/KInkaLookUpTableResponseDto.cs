using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    [AutoMap(typeof(KInkaLookUpTable))]
    public class KInkaLookUpTableResponseDto : PagedResultRequestDto
    {
        [JsonProperty("objId")]
        public long ObjId { get; set; }
        [JsonProperty("adi")]
        public string Adi { get; set; }

        [JsonProperty("aktif")]
        public bool? Aktif { get; set; }

        [JsonProperty("parentKodu")]
        public string  ParentKodu { get; set; }
    }
}
