using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.DocumentTypes.Dto
{
    [AutoMap(typeof(DocumentType))]
    public class DocumentTypeDto :  BaseEntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
