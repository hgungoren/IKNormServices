using System;
using Abp.AutoMapper;
using Serendip.IK.Common;

namespace Serendip.IK.TextTemplates.Dto
{
    [AutoMap(typeof(TextTemplate))]
    public class TextTemplateDto : BaseEntityDto
    {
        public TextTemplateDto()
        {
        }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Template { get; set; }

        public string Data { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public long? QuoteId { get; set; }

    }
}
