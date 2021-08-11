using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.File.Dto;
using Serendip.IK.Files.Dto;

namespace Serendip.IK.Files
{
    public interface IFileAppService : IAsyncCrudAppService<FileDto, long, PagedAndSortedResultRequestDto>
    {
        //Task<FileDownloadDto> Download(long id);

        //Task<string> GetPreviewLink(long id);
        //Task<bool> CheckPreviewToken(long id, string token);

        //int GetAllSize();
        //Task<FileStorageInfoDto> GetFileStorageInfo();
        //Task<StorageSizeAndLimitDto> GetStorageSizeAndLimit();
        //Task<FileStreamResult> GetStreamAsync(long id);
        //Task<FileStreamResult> _Download(long id);
        //Task<FileStreamResult> _DownloadByName(string fileName);
    }
}
