using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Helpers
{
    public static class Enum<T> where T : struct, IConvertible
    {
        public static Dictionary<string, string> GetLocalizationAndValues
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                var values = Enum.GetValues(typeof(T));
                Dictionary<string, string> response = new Dictionary<string, string>();
                foreach (var value in values)
                {
                    response.Add(value.ToString(), value.ToString());
                }
                return response;
            }
        }
    }
}
