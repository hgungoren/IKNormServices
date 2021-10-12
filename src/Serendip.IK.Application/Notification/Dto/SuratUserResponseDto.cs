using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification.Dto
{
    public class SuratUserResponseDto : SuratBaseResponseDto
    {
        public string Email { get; set; }

        public string Language { get; set; }
    }
}
