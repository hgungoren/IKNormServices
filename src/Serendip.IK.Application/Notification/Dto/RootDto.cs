using System;

namespace Serendip.IK.Notification.Dto
{
    public class NotifcationData
    {
        public int? talepNedeni { get; set; } 
        public int? talepTuru { get; set; } 
        public string pozisyon { get; set; } 
        public string personelId { get; set; } 
        public string aciklama { get; set; } 
        public int? normStatus { get; set; } 
        public string subeObjId { get; set; } 
        public int? talepDurumu { get; set; } 
        public long? bagliOlduguSubeObjId { get; set; } 
        public DateTime? creationTime { get; set; } 
        public long? id { get; set; } 
    }
    public class NotifcationDataq
    {
        public int? talepNedeni { get; set; }
        public int? talepTuru { get; set; }
        public string pozisyon { get; set; }
        public string personelId { get; set; }
        public string aciklama { get; set; }
        public int? normStatus { get; set; }
        public string subeObjId { get; set; }
        public int? talepDurumu { get; set; }
        public long? bagliOlduguSubeObjId { get; set; }
        public string creationTime { get; set; }
        public long? id { get; set; }
    }


    public class SendMailNotification
    {
        public string SiteUrl { get; set; }
        public Message Message { get; set; }
        public string ViewDetailText { get; set; }
        public string ViewDetailUrl { get; set; }
    }

    public class Message
    {
        public string NameKey { get; set; }
        public Namevalue NameValue { get; set; }
        public string Operation { get; set; }
        public string DateKey { get; set; }
        public DateTime DateValue { get; set; }
        public string DescriptionKey { get; set; }
        public Descriptionvalue DescriptionValue { get; set; }
        public string ErrorStatusCodeKey { get; set; }
        public string ErrorStatusCodeValue { get; set; }
        public object operatingUserValue { get; set; }
        public string operatingUserKey { get; set; }
    }

    public class Namevalue
    {
        public int talepNedeni { get; set; }
        public int talepTuru { get; set; }
        public string pozisyon { get; set; }
        public string aciklama { get; set; }
        public int normStatus { get; set; }
        public long subeObjId { get; set; }
        public int talepDurumu { get; set; }
        public long bagliOlduguSubeObjId { get; set; }
        public DateTime creationTime { get; set; }
        public int id { get; set; }
    }

    public class Descriptionvalue
    {
        public int talepNedeni { get; set; }
        public int talepTuru { get; set; }
        public string pozisyon { get; set; }
        public string aciklama { get; set; }
        public int normStatus { get; set; }
        public long subeObjId { get; set; }
        public int talepDurumu { get; set; }
        public long bagliOlduguSubeObjId { get; set; }
        public DateTime creationTime { get; set; }
        public int id { get; set; }
    }


}
