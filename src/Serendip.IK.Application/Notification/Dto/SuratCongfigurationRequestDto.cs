using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification.Dto
{
    public class SuratCongfigurationRequestDto
    {
        public List<ChannelProvider> ChannelProviders { get; set; } = new List<ChannelProvider>();

        public Application Application { get; set; }
    }
}
