using System;
using System.Collections.Generic;
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


        public static List<string> GetDisplayNames(this Enum enumValue, bool lower)
        {
            List<string> names = new List<string>();
            if (enumValue != null)
            {
                string retVal = "";
                retVal = enumValue.GetType()?
                                .GetMember(enumValue.ToString())?
                                .First()?
                                .GetCustomAttribute<DisplayAttribute>()?
                                .Name;

                //return lower ? retVal.ToLower() : retVal;

                return names;
            }
            return null;
        }
    }
}
