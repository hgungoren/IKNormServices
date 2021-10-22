using Serendip.IK.Users.Dto;
using System.Collections.Generic;

namespace Serendip.IK.Notification.Dto
{
    public class BaseSuratNotificationRequestDto
    {
        public UserDto To { get; set; }
        public string Url { get; set; } 
        public Application Application { get; set; }
        public List<SuratMessageRequestDto> Messages { get; set; }
    }
}
