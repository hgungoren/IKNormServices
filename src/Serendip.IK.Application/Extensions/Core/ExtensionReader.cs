using Abp.Dependency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serendip.IK.Extensions.Types.Button;
using Serendip.IK.Extensions.Types.Link;
using Serendip.IK.Extensions.Types.Mail;
using System.Collections.Generic;

namespace Serendip.IK.Extensions.Core
{
    public class ExtensionReader : ITransientDependency
    {
        public ExtensionDefination Read(string json)
        {
            var obj = JObject.Parse(json);
            var defination = ParseDefination(obj, null);
            defination.Items = ParseItems(obj);
            return defination;
        }

        public ExtensionDefination ParseDefination(JToken token, JArray configuration)
        {
            var defination = new ExtensionDefination();
            defination.Name = token["name"].ToString();
            defination.DisplayName = token["displayName"].ToString();
            defination.Description = token["description"].ToString();
            defination.Version = int.Parse(token["version"].HasValues ? token["version"].ToString() : "1");
            if (token["configurationFields"] != null && token["configurationFields"].HasValues)
            {
                foreach (var field in token["configurationFields"].Children())
                {
                    defination.ConfigurationFields.Add(new ExtensionItemConfigField()
                    {
                        Name = field["name"].ToString(),
                        DisplayName = field["displayName"].ToString(),
                        DefaultValue = field["defaultValue"].ToString()
                    });
                }
            }

            if (configuration != null && configuration.Count > 0)
            {
                foreach (var config in configuration)
                {
                    defination.Configuration.Add(new ExtensionConfiguration()
                    {
                        Name = config["Name"].ToString(),
                        Value = config["Value"].ToString()
                    });
                }

            }

            return defination;
        }

        public IExtensionItem ParseItem(JToken token)
        {
            //TODO : Add other items here
            switch (token["typeName"].ToString())
            {
                case ExtensionTypes.BUTTON:
                    return JsonConvert.DeserializeObject<ButtonExtension>(token.ToString());
                case ExtensionTypes.LINK:
                    return JsonConvert.DeserializeObject<LinkExtension>(token.ToString());
                case ExtensionTypes.MAIL:
                    return JsonConvert.DeserializeObject<MailExtension>(token.ToString());
                case ExtensionTypes.SMS:
                    return JsonConvert.DeserializeObject<SmsExtension>(token.ToString()); 
            }

            return null;
        }

        public List<IExtensionItem> ParseItems(JToken token)
        {
            var items = new List<IExtensionItem>();
            if (token["items"]?.HasValues ?? false)
            {
                foreach (var item in token["items"].Children())
                {
                    ToDefination(item);
                    var i = ParseItem(item);
                    i.Defination = item["defination"]?.ToString() ?? string.Empty;
                    items.Add(i);
                }
            }

            return items;
        }

        protected internal void ToDefination(JToken definationToken)
        {
            if (definationToken["defination"] != null)
                definationToken["defination"] = definationToken["defination"].ToString();
        }
    }
}