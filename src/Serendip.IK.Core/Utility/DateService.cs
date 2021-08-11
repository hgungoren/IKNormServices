using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Utility
{
    public class DateService : IDateService
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
