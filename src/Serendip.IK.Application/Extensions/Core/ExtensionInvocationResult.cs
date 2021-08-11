namespace Serendip.IK.Extensions.Core
{
    public class ExtensionInvocationResult
    {
        public long ExtensionId { get; set; }
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }
    }
}