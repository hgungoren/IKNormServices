using Abp.Domain.Uow;
using Serendip.IK.File.Dto;
using Serendip.IK.Files.Dto;
using Serendip.IK.MultiTenancy;
using Serendip.IK.MultiTenancy.Dto;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Serendip.IK.File
{
    public class FileUploadAppService : CoreAppServiceBase, IFileUploadAppService
    {
        private ITenantAppService _tenantAppService;
        private Task<TenantDto> currentTenant; 
        public FileUploadAppService(ITenantAppService tenantAppService) { }
         


        [UnitOfWork]
        public async Task<FileUploadResultDto> Upload(FileUploadDto dto)
        { 
            // get the file and convert it to the byte[]
            byte[] fileBytes;

            if (dto.DataArray.Length > 0)
            {
                fileBytes = dto.DataArray;
            }
            else
            {
                fileBytes = new Byte[dto.Data.Length];
                dto.Data.OpenReadStream().Read(fileBytes, 0, Int32.Parse(dto.Data.Length.ToString()));
            }
             
            var filePath = Guid.NewGuid().ToString().Replace("-", ""); 
            using (var stream = new MemoryStream(fileBytes))
            {
                try
                {
                    //var tenantName = TenantManager.GetByIdAsync(AbpSession.TenantId.Value).Result.TenancyName;


                    //var request = new PutObjectRequest
                    //{
                    //    BucketName = await _GetBucketName(),
                    //    //Key = await _GetFileKey(filePath),
                    //    Key = tenantName + "/" + filePath,
                    //    InputStream = stream,
                    //    ContentType = String.IsNullOrWhiteSpace(dto.ContentType) ? dto.Data.ContentType : dto.ContentType,
                    //    CannedACL = S3CannedACL.PublicRead
                    //};

                    //response = await client.PutObjectAsync(request);
                }
                catch (Exception ex)
                {
                    return new FileUploadResultDto()
                    {
                        Success = false,
                        Error = ex.Message
                    };
                }
            };

            //AWSXRayRecorder.Instance.EndSegment();
            //if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    // this model is up to you, in my case I have to use it following;
            //    return new FileUploadResultDto
            //    {
            //        Success = true,
            //        FileName = filePath,
            //        FileUrl = await _GetFileUrl(filePath)
            //    };
            //}
            //else
            //{
            //    // this model is up to you, in my case I have to use it following;
            //    return new FileUploadResultDto
            //    {
            //        Success = false,

            //    };
            //}

            return null;
        }

        async Task<string> _GetFileUrl(string filePath)
        {
            return $"https://{await _GetBucketName()}.{await SettingManager.GetSettingValueForTenantAsync("BA.CRM.Files.S3Host", AbpSession.TenantId.Value)}/{await _GetFileKey(filePath)}";
        }

        async Task<string> _GetBucketName()
        {
            return $"{await SettingManager.GetSettingValueForTenantAsync("BA.CRM.Files.S3Bucket", AbpSession.TenantId.Value)}";
        }

        async Task<string> _GetFileKey(string filePath)
        {
            var tenantName = TenantManager.GetByIdAsync(AbpSession.TenantId.Value).Result.TenancyName;
            return $"{tenantName}/{filePath}";
        }

        async Task<string> _GetAccessKeyId()
        {
            return $"{await SettingManager.GetSettingValueForTenantAsync("BA.CRM.Files.S3AccessKeyId", AbpSession.TenantId.Value)}";
        }

        async Task<string> _GetSecretAccessKey()
        {
            return $"{await SettingManager.GetSettingValueForTenantAsync("BA.CRM.Files.S3SecretAccessKey", AbpSession.TenantId.Value)}";
        }

        public async Task<FileDownloadDto> Download(long id)
        {
            return new FileDownloadDto();
        }

        public async Task DeleteFile(string path)
        {

            //var client = await GetClient();
            //var key = ParseKey(path);
            //var deleteObjectRequest = new DeleteObjectRequest
            //{
            //    BucketName = await _GetBucketName(),
            //    Key = await _GetFileKey(key)
            //};

            //await client.DeleteObjectAsync(deleteObjectRequest);
        }

        string ParseKey(string path)
        {
            var arr = path.Split('/');
            return arr[arr.Length - 1];
        } 
    }
}