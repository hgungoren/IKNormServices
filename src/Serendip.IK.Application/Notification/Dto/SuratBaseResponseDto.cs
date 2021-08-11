using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification.Dto
{
    public class SuratBaseResponseDto
    {
        public string Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UserId { get; set; }

        public string TenantId { get; set; }
    }
}
