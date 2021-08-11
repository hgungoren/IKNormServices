using Fare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Utility
{
    public static class UniqueNumberHelper
    {
        public static string DefaultFormat()
        {
            return "[0-9]{6}";
        }

        public static string GenerateUniqueNumber(string format="")
        {
            if (String.IsNullOrEmpty(format))
            {
                format = DefaultFormat();
            }

            var xeger = new Xeger(format);

            return xeger.Generate();
        }
        
        private static long _lastTime;
        
        private static object _timeLock = new object();
        internal static long GetCurrentTime() {
            lock ( _timeLock ) { // prevent concurrent access to ensure uniqueness
                DateTime result = DateTime.UtcNow;
                if ( result.Ticks <= _lastTime )
                    result = new DateTime( _lastTime + 1 );
                _lastTime = result.Ticks;
                return _lastTime;
            }
        }
    }
}
