using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}