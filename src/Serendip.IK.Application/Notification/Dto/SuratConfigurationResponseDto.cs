using System;
using System.Collections.Generic;

namespace Serendip.IK.Notification.Dto
{
    class SuratConfigurationResponseDto
    {
        public string Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UserId { get; set; }

        public string TenantId { get; set; }

        public List<ChannelProvider> ChannelProviders { get; set; }

        public Application Application { get; set; }
    }

    public class ChannelProvider
    {
        public Channel Channel { get; set; }

        public Provider Provider { get; set; }
    }

    public enum Provider
    {
        AwsSes = 1,
        Firebase,
        Smtp,
    }
}
