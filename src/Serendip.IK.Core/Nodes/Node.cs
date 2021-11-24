using Serendip.IK.Common;
using Serendip.IK.Positions;

namespace Serendip.IK.Nodes
{
    public class Node : BaseEntity
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string SubTitle { get; set; }
        public bool Expanded { get; set; }
        public int OrderNo { get; set; }
        public long PositionId { get; set; }

        /// <summary>
        /// Kullanıcıya Mail Gönderilecekmi
        /// </summary>
        public bool Mail { get; set; }


        /// <summary>
        /// Kullanıcıya Durum Değiştiğinde Mail Gönderilecekmi
        /// </summary>
        public bool MailStatusChange { get; set; }


        /// <summary>
        /// Kullanıcının Telefonuna Bildirim Gönderilecekmi
        /// </summary>
        public bool PushNotificationPhone { get; set; }

        /// <summary>
        /// Kullanıcının Durum Değiştiğinde Telefonuna Bildirim Gönderilecekmi
        /// </summary>
        public bool PushNotificationPhoneStatusChange { get; set; }


        /// <summary>
        /// Kullanıcının Web Browser'ına Bildirim Gönderilecekmi
        /// </summary>
        public bool PushNotificationWeb { get; set; }

        /// <summary>
        /// Kullanıcının Web Browser'ına urum Değiştiğinde Bildirim Gönderilecekmi
        /// </summary>
        public bool PushNotificationWebStatusChange { get; set; }


        public bool Active { get; set; }

        /// <summary>
        /// Kullanıcı İlgili Talebi Sonlandırabilirmi
        /// </summary>
        public bool CanTerminate { get; set; }

        /// <summary>
        /// Seçili pozisyonları belirtmeye yarar.
        /// </summary>
        public bool Selected { get; set; }
        public Position Position { get; set; }
    }
}
