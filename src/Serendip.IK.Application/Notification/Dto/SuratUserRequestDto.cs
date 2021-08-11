using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification.Dto
{
    public class SuratUserRequestDto
    {
        public string Language { get; set; }

        public Application Application { get; set; }
    }

    public enum Application
    {
        Crm,
        Lms
    }
}
