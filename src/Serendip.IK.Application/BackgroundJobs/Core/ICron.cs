using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.BackgroundJobs.Core
{
    public interface ICron
    {
        void Invoke();
    }
}
