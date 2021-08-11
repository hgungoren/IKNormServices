using System;
using System.Collections.Generic;

namespace Serendip.IK.Extensions.Services.Button
{
    public class ButtonInvokeRequest
    {
        public string ModelName { get; set; }

        public string ModelId { get; set; }

        public List<long> Selecteds { get; set; }

        public string ExtensionId { get; set; }
    }
}