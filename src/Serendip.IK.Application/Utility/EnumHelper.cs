using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Serendip.IK.Utility
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumValue, bool lower)
        {
            if (enumValue != null)
            {
                string retVal = "";
                retVal = enumValue.GetType()?
                                .GetMember(enumValue.ToString())?
                                .First()?
                                .GetCustomAttribute<DisplayAttribute>()?
                                .Name;

                return lower ? retVal.ToLower() : retVal;
            }

            return "";
        }
    }
}
