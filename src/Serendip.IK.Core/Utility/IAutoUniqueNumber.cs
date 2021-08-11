using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Utility
{
    public interface IAutoUniqueNumber
    {
        string UniqueNumber { get; set; }

        string UniqueNumberFormat();
    }
}
