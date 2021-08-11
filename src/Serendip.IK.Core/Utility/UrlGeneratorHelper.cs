using Abp.Dependency;
using Abp.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Serendip.IK.Utility
{
    public class UrlGeneratorHelper: IDomainService, ITransientDependency
    {
        private IConfiguration _config;

        public UrlGeneratorHelper(IConfiguration config)
        {
            _config = config;
        }
        public string BaseUrl()
        {
            return _config.GetValue<string>("ApplicationUrl");
        }
        public string GenerateUrl(string action,string controller,object param)
        {
            //TODO : Generic hale getir.

            var url = $"{BaseUrl()}/{controller}/{action}";
            if (param != null)
            {
                url = $"{url}{ToQueryString(param)}";
            }
            return url;
        }

        public string ToQueryString(object obj)
        {

            StringBuilder queryStringBuilder = new StringBuilder("?");

            Type objType = obj.GetType();
            // Get properties marked with `[QueryString]`
            List<PropertyInfo> props = objType.GetProperties()
                .ToList();

            foreach (var prop in props)
            {
                string name = prop.Name;
                var value = prop.GetValue(obj, null);

                if (value != null)
                {
                    // Check's if this is the last property, if so, don't add an '&'
                    if (props.IndexOf(prop) != (props.Count - 1))
                    {
                        queryStringBuilder.Append($"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value.ToString())}&");
                    }
                    else
                    {
                        queryStringBuilder.Append($"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(value.ToString())}");
                    }
                }
            }

            return queryStringBuilder.ToString();
        }
    }
}
