using System;

namespace Serendip.IK.Extensions.Services.Link
{
    public class LinkDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public long Id { get; set; }

        public string ExtensionType { get; set; } = "Link";
    }
}