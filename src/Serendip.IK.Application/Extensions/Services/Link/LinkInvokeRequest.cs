using System;
using System.Collections.Generic;

namespace Serendip.IK.Extensions.Services.Link
{
    public class LinkInvokeRequest
    {
        public string ModelName { get; set; }

        public string ModelId { get; set; }

        public string ExtensionId { get; set; }
    }
}