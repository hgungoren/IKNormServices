using Abp.Localization;

namespace Serendip.IK.BackgroundJobs.Core
{
    public class JobContext<TData>
    {
        public long? UserId { get; set; }

        public int? TenantId { get; set; }
        public TData Data { get; set; }
        
        public LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }
    }
}