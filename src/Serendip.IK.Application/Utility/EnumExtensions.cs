using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Utility
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        public static string GetDescription(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }

        public static List<EnumViewModel> GetEnumList<T>()
        {
            List<EnumViewModel> enumViews = new List<EnumViewModel>();
            // var list = new List<KeyValuePair<string, byte>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumViewModel viewModel = new EnumViewModel();
                viewModel.id = (byte)e;
                viewModel.name = (((Enum)e).GetDisplayName());

                //list.Add(new KeyValuePair<string, byte>(((Enum)e).GetDisplayName(), (byte)e));
                enumViews.Add(viewModel);
            }
            return enumViews;
        }
    }


    public class EnumViewModel
    {

        public byte id { get; set; }
        public string name { get; set; }
    }




}
