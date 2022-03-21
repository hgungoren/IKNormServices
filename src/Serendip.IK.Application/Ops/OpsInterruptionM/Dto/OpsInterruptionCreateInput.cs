using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using Serendip.IK.Ops.Interruption;
using Newtonsoft.Json;

namespace Serendip.IK.Ops.OpsInterruptionM.Dto
{
   

    [AutoMap(typeof(OpsInterruption))]
    public class OpsInterruptionCreateInput
    {

        [JsonProperty("kesintibirimi")]
        public string Kesintibirimi { get; set; }

        [JsonProperty("kesintibirimkodu")]
        public string Kesintibirimkodu { get; set; }

        [JsonProperty("kesintiyapilacakunvan")]
        public string Kesintiyapilacakunvan { get; set; }

        [JsonProperty("calismabaslangictarihi")]
        public DateTime Calismabaslangictarihi { get; set; }

        [JsonProperty("calismabitistarihi")]
        public DateTime Calismabitistarihi { get; set; }

        [JsonProperty("kesintiorani")]
        public int Kesintiorani { get; set; }

        [JsonProperty("tutar")]
        public float Tutar { get; set; }

        [JsonProperty("tazminId")]
        public int TazminId { get; set; }

    }


    [AutoMap(typeof(OpsInterruption))]
    public class OpsInterruptionDto : BaseEntityDto
    {

        [JsonProperty("kesintibirimi")]
        public string Kesintibirimi { get; set; }

        [JsonProperty("kesintibirimkodu")]
        public string Kesintibirimkodu { get; set; }

        [JsonProperty("kesintiyapilacakunvan")]
        public string Kesintiyapilacakunvan { get; set; }

        [JsonProperty("calismabaslangictarihi")]
        public DateTime Calismabaslangictarihi { get; set; }

        [JsonProperty("calismabitistarihi")]
        public DateTime Calismabitistarihi { get; set; }

        [JsonProperty("kesintiorani")]
        public int Kesintiorani { get; set; }

        [JsonProperty("tutar")]
        public float Tutar { get; set; }

        [JsonProperty("tazminId")]
        public int TazminId { get; set; }
    }


    public class OpsPagedKNormResultRequestDto : PagedAndSortedResultRequestDto { }

}
