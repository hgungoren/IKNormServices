using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.TextTemplates
{
    public class TextTemplate : BaseEntity, IMustHaveTenant
    {
        public TextTemplate()
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

        public int TenantId { get; set; }
    }
}
