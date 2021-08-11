using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using Serendip.IK.Common;
using Serendip.IK.Files;
using Serendip.IK.Files.Dto;
using System;

namespace Serendip.IK.File.Dto
{
    [AutoMap(typeof(Files.File))]
    public class FileDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }

        public string Link { get; set; }

        public string DownloadLink
        {
            get
            {
                return "document_download/";
            }
        }
        public string ParentType { get; set; }
        public int? Size { get; set; }

        public string SizeText
        {
            get
            {
                if (Size == null)
                {
                    return "0 MB";
                }
                var kb = Size / 1024;

                var mb = kb / 1024;

                if (mb < 1)
                {
                    return $"{kb} KB";
                }

                return $"{mb} MB";


            }
        }
        public long? ParentId { get; set; }
        public long? FolderId { get; set; }
        public FileType Type { get; set; } = FileType.File;

        public AccessLevel AccessLevel { get; set; }
        public long OwnerId { get; set; }
        public long OwnerGroupId { get; set; }

        public IFormFile Data { get; set; }

        public byte[] DataArray { get; set; }
        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public FileDto()
        {
            DataArray = new byte[0];
            this.Capabilities = new Capabilities
            {
                CanDownload = true,
                CanEdit = true,
                CanDelete = true
            };
        }

        public Capabilities Capabilities { get; set; }
    }
}
