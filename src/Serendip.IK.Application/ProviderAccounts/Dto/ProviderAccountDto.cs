using Abp.AutoMapper;
using Newtonsoft.Json;
using Serendip.IK.Common;

namespace Serendip.IK.ProviderAccounts.Dto
{
    [AutoMap(typeof(ProviderAccount))]
    public class ProviderAccountDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Provider { get; set; }

        public string ConfigurationData { get; set; }

        public long OwnerId { get; set; }

        public T GetConfiguration<T>()
        {
            return JsonConvert.DeserializeObject<T>(ConfigurationData);
        }
        public void GetConfiguration<T>(T configuration)
        {
            ConfigurationData = JsonConvert.SerializeObject(configuration);
        }
    }
}