using Abp.Application.Services;
using Serendip.IK.File.Dto;
using Serendip.IK.Files.Dto;
using System;
using System.Threading.Tasks;

namespace Serendip.IK.File
{
    public interface IFileUploadAppService:IApplicationService
    {
        Task<FileUploadResultDto> Upload(FileUploadDto dto);
        Task<FileDownloadDto> Download(long id);
        Task DeleteFile(string path);
    }
}
