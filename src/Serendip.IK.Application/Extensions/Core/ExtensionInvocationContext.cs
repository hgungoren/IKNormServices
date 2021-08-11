namespace Serendip.IK.Extensions.Core
{
    public class ExtensionInvocationContext
    {
        public int TenantId { get; set; }

        public long? UserId { get; set; }

        public string TenantName { get; set; }

        public ExtensionDefination ExtensionDefination { get; set; }
    }
}