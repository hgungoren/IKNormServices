using Abp.AutoMapper;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.ActivityLoggers.Dto
{
    [AutoMap(typeof(ActivityLogger))]
    public class ActivityLoggerDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ModelName { get; set; }
        public string ModelId { get; set; }
        public string ReferenceModel { get; set; }
        public string ReferenceID { get; set; }
        public string DisplayKey { get; set; }
        public string DisplayParams { get; set; }
        public string[] DisplayValues
        {
            //get
            //{
            //    return DisplayParams.Split('|');
            //}
            //set
            //{
            //    if (value == null)
            //    {
            //        return;
            //    }
            //    DisplayParams = String.Join("|", value);
            //}
            get;set;
        }
        public DateTime Date { get; set; }
    }
}
