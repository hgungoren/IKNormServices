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
        //public string NameKey { get; set; }
        //public string NameValue { get; set; }
        //public string Operation { get; set; }
        //public string DateKey { get; set; }
        //public DateTime DateValue { get; set; }
        //public string DescriptionKey { get; set; }
        //public string DescriptionValue { get; set; }
        //public string ErrorStatusCodeKey { get; set; }
        //public string ErrorStatusCodeValue { get; set; }
        //public object OperatingUserValue { get; set; }
        //public string OperatingUserKey { get; set; }
        public int talepNedeni { get; set; } = 0;
        public int talepTuru { get; set; } = 0;
        public string pozisyon { get; set; } = "Yok";
        public string personelId { get; set; } = "Yok";
        public string aciklama { get; set; } = "Yok";
        public int normStatus { get; set; } = 0;
        public string subeObjId { get; set; } = "Yok";
        public int talepDurumu { get; set; } = 0;
        public long bagliOlduguSubeObjId { get; set; } = 0;
        public DateTime creationTime { get; set; } = DateTime.Now;
        public long id { get; set; } = 0;
    }


}
