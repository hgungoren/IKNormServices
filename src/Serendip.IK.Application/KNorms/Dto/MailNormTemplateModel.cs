using System;

namespace Serendip.IK.KNorms.Dto
{
    public class MailNormTemplateModel
    {
        public string SiteUrl { get; set; }
        public Message Message { get; set; }
        public string ViewDetailText { get; set; }
        public string ViewDetailUrl { get; set; }
    }


    public class Message
    {
        public string NameKey { get; set; }
        public string NameValue { get; set; }
        public string Operation { get; set; }
        public string DateKey { get; set; }
        public DateTime DateValue { get; set; }
        public string DescriptionKey { get; set; }
        public string DescriptionValue { get; set; }
        public string ErrorStatusCodeKey { get; set; }
        public string ErrorStatusCodeValue { get; set; }
        public object OperatingUserValue { get; set; }
        public string OperatingUserKey { get; set; }
    }


}
