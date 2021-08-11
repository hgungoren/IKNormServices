using Serendip.IK.Extensions.Core;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Extensions.Dto
{
    public class ExtensionConfigurationParameter
    {
        public long Id { get; set; }
        public List<ExtensionConfiguration> Configurations { get; set; } = new List<ExtensionConfiguration>();

    }
}