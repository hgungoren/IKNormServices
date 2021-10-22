using System;

namespace Serendip.IK.KNorms.Dto
{
     
    public class Message
    {
        public string SourceName { get; set; }
        public string Name { get; set; }
    }
     
    public class MailNormTemplateModel
    {
        public Message Message { get; set; }
        public string Type { get; set; }
        public Properties Properties { get; set; }
    }
     
    public class Properties
    {
        public string Detail { get; set; }
        public string Url { get; set; }
        public string FootNote { get; set; }
    }



    public class Detail
    {
        public int TalepNedeni { get; set; }
        public int TalepTuru { get; set; }
        public string Pozisyon { get; set; }
        public string Aciklama { get; set; }
        public int NormStatus { get; set; }
        public long SubeObjId { get; set; }
        public int TalepDurumu { get; set; }
        public long BagliOlduguSubeObjId { get; set; }
        public DateTime CreationTime { get; set; }
        public int Id { get; set; }
    }

}
