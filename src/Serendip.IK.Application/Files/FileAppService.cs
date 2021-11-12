using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Serendip.IK.File;
using Serendip.IK.File.Dto;
using Serendip.IK.Files.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Files
{
    public class FileAppService : CoreAsyncCrudAppService<File, FileDto, long>, IFileAppService
    {
        private IRepository<File, long> _repository;
        private IFileUploadAppService _fileUploadAppService;
        private ICacheManager _cacheManager;

        public FileAppService(IRepository<File, long> repository, IFileUploadAppService fileUploadAppService, ICacheManager cacheManager) : base(repository)
        {
            _repository = repository;
            _fileUploadAppService = fileUploadAppService;
            _cacheManager = cacheManager;
        }





        /*
        protected override IQueryable<File> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input).Include(x => x.CreatorUserId);
        }

        private async Task CheckSizeLimit()
        {

            int currentSize = await Repository.GetAll().SumAsync(x => x.Size ?? 0);
            CheckSizeLimit(currentSize);
        }

        [AbpAuthorize(PermissionNames.document_create)]
        [UnitOfWork]
        public async override Task<FileDto> Create(FileDto input)
        {
            await CheckSizeLimit();
            FileUploadResultDto response = null;
            if (input.Type == FileType.File)
            {
                if (input.DataArray.Length > 0)
                {
                    if (!await CheckSize(input.DataArray.Length))
                    {
                        throw new Exception("");// LicenseException("file-size", "Max File Size exceeded");
                    }

                    response = await _fileUploadAppService.Upload(new FileUploadDto
                    {
                        DataArray = input.DataArray,
                        ContentType = input.ContentType,
                        FileName = input.FileName,
                    });
                }
                else
                {
                    if (!await CheckSize((int)input.Data.Length))
                    {
                        throw new Exception("");//LicenseException("file-size", "Max File Size exceeded");
                    }

                    response = await _fileUploadAppService.Upload(new FileUploadDto { Data = input.Data });
                }

                if (!response.Success)
                {
                    throw new UserFriendlyException(response.Error);
                }

                input.Link = response.FileUrl;

                if (input.DataArray.Length > 0)
                {
                    input.FileName = System.IO.Path.GetFileName(input.FileName);
                    input.Extension = System.IO.Path.GetExtension(input.FileName);
                    input.Size = input.DataArray.Length;
                }
                else
                {
                    input.FileName = System.IO.Path.GetFileName(input.Data.FileName);
                    input.Extension = System.IO.Path.GetExtension(input.Data.FileName);
                    input.Size = (int)input.Data.Length;
                }
            }
            else
            {
                input.Extension = "folder";
            }

            await CheckUserGroup(input);
            var result = await base.CreateAsync(input);
            EventBus.Trigger(GetEventParameter(new EventHandlerEto<File>
            {
                EventName = EventNames.DOCUMENT_CREATED,
                Entity = MapToEntity(result),
                LogType = ActivityLoggerTypes.ITEM_ADDED,
                DisplayKey = "Log_File_Added",
                ReferenceID = result.ParentId.HasValue ? result.ParentId.ToString() : "",
                ReferenceModel = result.ParentType != null ? result.ParentType : "document",
            }));
            return result;
        }

        //private async Task CheckUserGroup(FileDto input)
        //{
        //    var userUserGroupRepo = IocManager.Instance.Resolve<IUserUserGroupRepository>();
        //    var ownerUserGroup = userUserGroupRepo.GetCurrentUserUserGroup(AbpSession.UserId.Value, AbpSession.TenantId.Value);
        //    if (ownerUserGroup != null)
        //    {
        //        input.OwnerGroupId = ownerUserGroup.UserGroupId;
        //    }
        //    else
        //    {
        //        var userGroupRepo = IocManager.Instance.Resolve<IUserGroupRepository>();
        //        var tenantUserGroup = userGroupRepo.GetCurrentTenantUserGroup(AbpSession.TenantId.Value);
        //        input.OwnerGroupId = tenantUserGroup.Id;
        //    }
        //}

        [AbpAuthorize(PermissionNames.document_delete)]
        public override async Task Delete(EntityDto<long> input)
        {
            var file = Repository.Get(input.Id)
                ?? throw new Exception("");// EntityNotFoundException(nameof(File) + L("DataNotFound"), instance: input.Id.ToString());
            await _fileUploadAppService.DeleteFile(file.Link);
            await base.DeleteAsync(input);
            EventBus.Trigger(GetEventParameter(new EventHandlerEto<File>
            {
                EventName = EventNames.DOCUMENT_DELETED,
                Entity = file,
                LogType = ActivityLoggerTypes.ITEM_REMOVED,
                DisplayKey = "Log_File_Removed",
                ReferenceID = file.ParentId.HasValue ? file.ParentId.ToString() : "",
                ReferenceModel = string.IsNullOrEmpty(file.ParentType) ? string.Empty : file.ParentType
            }));
        }
        [AbpAuthorize(PermissionNames.document_view)]
        public async override Task<FileDto> Get(EntityDto<long> input)
        {
            FileDto dto = new FileDto();

            await Task.Run(() =>
            {
                dto = ObjectMapper.Map<FileDto>(_repository.GetAll().Include(x => x.CreatorUserId)
                    .Where(x => x.Id == input.Id)
                    .AsNoTracking()
                    .SingleOrDefault())
                ?? throw new Exception("");// EntityNotFoundException(nameof(File) + L("DataNotFound"), instance: input.Id.ToString());
            });
            return dto;
        }
        [AbpAuthorize(PermissionNames.document_view)]
        public async Task<PagedResultDto<FileDto>> GetAll(GetAllFileInput input)
        {
            var q = Repository.GetAll().Include(x => x.Owner).AsQueryable().AsNoTracking();


            if (!String.IsNullOrEmpty(input.ParentType))
            {
                q = q.Where(x => x.ParentType == input.ParentType);
            }

            if (input.ParentId != null)
            {
                q = q.Where(x => x.ParentId == input.ParentId.Value);
            }

            if (input.Extensions != null && input.Extensions.Length > 0)
            {
                //input.Extensions.ForEach(x => x = String.Format($"%{x.ToLower()}%"));
                //q = q.Where(x => EF.Functions.ILike(x.Extension, input.Extensions));
                //q = q.Where(x => input.Extensions.IndexOf(x.Extension, StringComparison.OrdinalIgnoreCase));
                q = q.Where(x => input.Extensions.Contains(x.Extension));
            }


            q = q.Where(x => x.FolderId == input.FolderId);

            var items = q.OrderBy(x => x.FileName).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var mapped = ObjectMapper.Map<List<FileDto>>(items);
            return new PagedResultDto<FileDto>
            {
                Items = mapped,
                TotalCount = Repository.Count()
            };
        }

        public async Task<FileDownloadDto> Download(long id)
        {
            var file = Repository.Get(id)
                ?? throw new Exception("");//new EntityNotFoundException(nameof(File) + L("DataNotFound"), instance: id.ToString());

            var net = new System.Net.WebClient();
            var data = net.DownloadData(file.Link);
            return new FileDownloadDto
            {
                Name = file.FileName,
                Data = data
            };
        }


        public int GetAllSize()
        {
            return Repository.GetAll().Sum(x => x.Size).GetValueOrDefault();
        }

        async Task<bool> CheckSize(int size)
        {
            return true;
            //var currentSize = GetAllSize();

            //var result = await SettingManager.GetSettingValueForTenantAsync("Serendip.IK.MaxFileSize", AbpSession.TenantId.Value);
            //if (!string.IsNullOrWhiteSpace(result))
            //{
            //    var maxSize = int.Parse(result);
            //    if (maxSize > currentSize + size)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}

            //return false;
        }

        public async Task<StorageSizeAndLimitDto> GetStorageSizeAndLimit()
        {
            var instance = IocManager.Instance;
            //var leadRepository = instance.Resolve<IRepository<Leads.Lead, long>>();
            //var opportunityRepository = instance.Resolve<IRepository<Opportunities.Opportunity, long>>();
            //var accountRepository = instance.Resolve<IRepository<Customers.Account, long>>();
            //var contactRepository = instance.Resolve<IRepository<Contacts.Contact, long>>();
            //var productRepository = instance.Resolve<IRepository<Products.Product, long>>();
            //var activityRepository = instance.Resolve<IRepository<Activity.Activity, long>>();
            //var paymentRepository = instance.Resolve<IRepository<Payments.Payment, long>>();
            //var invoiceRepository = instance.Resolve<IRepository<Invoices.Invoice, long>>();
            var limit = new StorageSizeAndLimitDto()
            {
                TotalRowLimit = FeatureChecker.GetValue("Row_Limit").To<int>(),
                UsageRowLimit = new List<RowLimitDto>()
                {
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.LEAD,
                    //    Count = await leadRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.OPPORTUNITY,
                    //    Count = await opportunityRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.ACCOUNT,
                    //    Count = await accountRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.CONTACT,
                    //    Count = await contactRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.PRODUCT,
                    //    Count = await productRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.ACTIVITY,
                    //    Count = await accountRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.PAYMENT,
                    //    Count = await paymentRepository.CountAsync(x=>!x.IsDeleted)
                    //},
                    //new RowLimitDto()
                    //{
                    //    Model = ModelTypes.INVOICE,
                    //    Count = await invoiceRepository.CountAsync(x=>!x.IsDeleted)
                    //}
                }
            };
            return limit;
        }

        public async Task<FileStorageInfoDto> GetFileStorageInfo()
        {
            var currentSize = GetAllSize();
            var maxSize = int.Parse(await SettingManager.GetSettingValueForTenantAsync("Serendip.IK.MaxFileSize", AbpSession.TenantId.Value));
            return new FileStorageInfoDto()
            {
                CurrentSize = currentSize,
                MaxSize = maxSize
            };
        }

        public async Task<string> GetPreviewLink(long id)
        {
            var token = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).ToLower().Replace("-", "");
            _cacheManager.GetCache("preview_token").Set(id.ToString(), token, slidingExpireTime: TimeSpan.FromMinutes(10));

            return "";// UrlGenerator.Url("file_preview", new { id = id, token = token });
        }

        public async Task<bool> CheckPreviewToken(long id, string token)
        {
            var _token = _cacheManager.GetCache("preview_token").GetOrDefault(id.ToString());
            return token == _token;
        }

        public async Task<FileStreamResult> GetStreamAsync(long id)
        {
            var file = Repository.Get(id)
                ?? throw new Exception("");// new EntityNotFoundException(nameof(File) + L("DataNotFound"), instance: id.ToString());
            if (AbpSession.UserId != null && file.CreatorUserId != AbpSession.UserId.Value)
            {
                throw new UnauthorizedAccessException();
            }
            var net = new System.Net.WebClient();
            var data = net.DownloadData(file.Link);
            Stream stream = new MemoryStream(data);

            return new FileStreamResult(stream, file.ContentType)
            {
                FileDownloadName = file.FileName
            };

        }
        public async Task<FileStreamResult> _Download(long id)
        {
            var file = Repository.Get(id)
                ?? throw new Exception("");// new EntityNotFoundException(nameof(File) + L("DataNotFound"), instance: id.ToString());
            var net = new System.Net.WebClient();
            var data = net.DownloadData(file.Link);
            Stream stream = new MemoryStream(data);

            return new FileStreamResult(stream, file.ContentType)
            {
                FileDownloadName = file.FileName
            };

        }

        public async Task<FileStreamResult> _DownloadByName(string fileName)
        {
            var file = Repository.GetAll().FirstOrDefault(x => x.FileName == fileName);
            var net = new System.Net.WebClient();
            var data = net.DownloadData(file.Link);
            Stream stream = new MemoryStream(data);

            return new FileStreamResult(stream, file.ContentType)
            {
                FileDownloadName = file.FileName
            };

        }*/
    }
}
