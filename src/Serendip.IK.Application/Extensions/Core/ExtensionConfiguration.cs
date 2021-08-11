using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.Extensions.Core
{
    public class ExtensionConfiguration
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}