using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace Serendip.IK.KPersonels.Dto
{
    [AutoMap(typeof(KPersonelDto))]
    public class KPersonelResponseDto : PagedResultRequestDto
    {
        [JsonProperty("objId")]
        public string ObjId { get; set; }


        [JsonProperty("ad")]
        public string Ad { get; set; }


        [JsonProperty("soyad")]
        public string Soyad { get; set; }

         

        [JsonProperty("aktif")]
        public bool? Aktif { get; set; }



        [JsonProperty("gorevi")]
        public string Gorevi { get; set; }

        [JsonProperty("isYeri_ObjId")]
        public string IsYeri_ObjId { get; set; }


        [JsonProperty("sicilNo")]
        public string SicilNo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }


        [JsonProperty("kullanici_ObjId")]
        public long Kullanici_ObjId { get; set; } 
    }
}
