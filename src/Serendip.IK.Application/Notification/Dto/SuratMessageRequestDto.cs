using System.Collections.Generic;

namespace Serendip.IK.Notification.Dto
{
    public class SuratMessageRequestDto
    {
        public Channel Channel { get; set; }

        public List<SuratLocalizedField> Title { get; set; }

        public List<SuratLocalizedField> Body { get; set; }

        public List<SuratLocalizedField> Attachment { get; set; }
    }

    public enum Channel
    {
        /// <summary>
        /// Push Notification
        /// </summary>
        Push = 1,
        /// <summary>   
        /// Email
        /// </summary>
        Email,
        /// <summary>
        /// Sms
        /// </summary>
        Sms,
        /// <summary>
        /// Web Push (Notification)
        /// </summary>
        Web
    }
}
