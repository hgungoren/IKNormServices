using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.Extensions.Dto
{
    /// <summary>
    /// This Data Transfer Object uses for Button And Link Extension.
    /// </summary>
    public class InvokeRequestDto
    {
        [Required]
        public string ModelName { get; set; }

        [Required]
        public string ModelId { get; set; }

        [Required]
        public string ExtensionId { get; set; }
    }
}
