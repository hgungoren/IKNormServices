using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serendip.IK.Extensions.Core;
using Serendip.IK.Extensions.Dto;
using Serendip.IK.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions
{
    public class ExtensionManager : IExtensionManager
    {
        private IAbpSession _abpSession;
        private IRepository<Extension, long> _extensionRepository;
        private IRepository<ExtensionItem, long> _extensionItemRepository;
        private ExtensionReader _extensionReader;
        private IObjectMapper _objectMapper;
        private readonly ICurrentUnitOfWorkProvider currentUnitOfWorkProvider;
        private IRepository<MarketplaceItem, long> _marketplaceItemRepository;

        public ExtensionManager(
            IAbpSession abpSession
            , IRepository<Extension, long> extensionRepository
            , IRepository<ExtensionItem, long> extensionItemRepository
            , IRepository<MarketplaceItem, long> marketplaceItemRepository
            , ExtensionReader extensionReader
            , IObjectMapper objectMapper,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider
            )
        {
            _abpSession = abpSession;
            _extensionRepository = extensionRepository;
            _extensionItemRepository = extensionItemRepository;
            _extensionReader = extensionReader;
            _objectMapper = objectMapper;
            this.currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _marketplaceItemRepository = marketplaceItemRepository;
        }


        public async Task<ExtensionInvocationResult> Invoke(string name, object data)
        {

            var itemEntity = _extensionItemRepository.GetAllIncluding(x => x.Extension)
                .Where(x => x.Extension.Status == ExtensionStatus.Active)
                .FirstOrDefault(x => x.TypeName == name);


            if (itemEntity == null)
            {
                throw new ExtensionNotInstalledException(name);
            }

            return await InvokeItem(itemEntity, data);
        }

        //[HttpPost]
        //public async Task<ExtensionInvocationResult> Invoke(long id, object data)
        //{
        //    var itemEntity = _extensionItemRepository.GetAllIncluding(x => x.Extension)
        //        .Where(x => x.Extension.Status == ExtensionStatus.Active)
        //        .FirstOrDefault(x => x.Id == id);

        //    if (itemEntity == null)
        //    {
        //        throw new ExtensionNotInstalledException(id.ToString());
        //    }

        //    return await InvokeItem(itemEntity, data);
        //}


        async Task<ExtensionInvocationResult> InvokeItem(ExtensionItem itemEntity, object data)
        {
            IExtensionItem item = null;
            ExtensionDefination definaton = null;
            item = _extensionReader.ParseItem(JToken.Parse(itemEntity.Defination));
            JArray configuration = null;
            if (!String.IsNullOrEmpty(itemEntity.Extension.Configuration))
            {
                //itemEntity.Extension.Configuration = CommonHelper.Decrypt(itemEntity.Extension.Configuration);
                configuration = JArray.Parse(itemEntity.Extension.Configuration);
            }
            definaton = _extensionReader.ParseDefination(JToken.Parse(itemEntity.Extension.Defination), configuration);
            item.Id = itemEntity.Id;
            if (item == null)
            {
                //TODO : Not Valid Exception
                throw new Exception("Extension Item Not Valid");
            }

            var context = new ExtensionInvocationContext()
            {
                UserId = _abpSession.GetUserId(),
                TenantId = _abpSession.GetTenantId(),
                ExtensionDefination = definaton
            };
            var result = await item.Invoke(context, data);
            result.ExtensionId = itemEntity.ExtensionId;

            return result;
        }


        public async Task<List<IExtensionItem>> GetItems(string name)
        {
            var itemEntities = _extensionItemRepository.GetAllIncluding(x => x.Extension)
                .Where(x => x.Extension.Status == ExtensionStatus.Active)
                .Where(x => x.TypeName == name);
            var items = new List<IExtensionItem>();

            foreach (var itemEntity in itemEntities)
            {
                var item = _extensionReader.ParseItem(JToken.Parse(itemEntity.Defination));
                item.Id = itemEntity.Id;

                items.Add(item);
            }
            return items;
        }

        public async Task<ExtensionDefination> Install(string data)
        {
            return await _Install(data);
        }

        async Task<ExtensionDefination> _Install(string data, long? marketplaceItemId = null)
        {
            var defination = _extensionReader.Read(data);

            var extension = new Extension();
            extension.Name = defination.Name;
            extension.Description = defination.Description;
            extension.DisplayName = defination.DisplayName;
            extension.Version = defination.Version;
            extension.InstalledDate = DateTime.UtcNow;
            extension.Status = ExtensionStatus.Disabled;
            extension.Defination = data;
            extension.MarketplaceItemId = marketplaceItemId;

            var entity = await _extensionRepository.InsertAsync(extension);
            await currentUnitOfWorkProvider.Current.SaveChangesAsync();
            foreach (var item in defination.Items)
            {
                var i = new ExtensionItem();
                i.TypeName = item.TypeName;
                i.Name = item.Name;
                i.DisplayName = item.DisplayName;
                i.Extension = extension;
                i.Defination = item.Defination;
                i.ExtensionId = entity.Id;
                await _extensionItemRepository.InsertAsync(i);
            }

            foreach (var item in defination.Items)
            {
                defination.Id = entity.Id;
                await item.Install(defination);
            }
            defination.Id = extension.Id;

            return defination;
        }

        public bool IsInstalled(string name)
        {
            //TODO : Item olarak yüklü mü kontrol etmek lazım.
            return false;
        }

        public async Task<List<ExtensionDto>> GetExtensions()
        {
            var extensions = _extensionRepository.GetAllIncluding(x => x.Items, x => x.Owner).ToList();

            return extensions.Select(x => _objectMapper.Map<ExtensionDto>(x)).ToList();
        }

        public async Task<List<MarketplaceItemDto>> GetMarketplace()
        {
            var items = _marketplaceItemRepository.GetAll().ToList();
            return items.Select(x => _objectMapper.Map<MarketplaceItemDto>(x)).ToList();
        }

        public async Task<MarketplaceItemDto> GetMarketplaceItem(long id)
        {
            var item = _marketplaceItemRepository.GetAll().FirstOrDefault(x => x.Id == id);
            return _objectMapper.Map<MarketplaceItemDto>(item);
        }

        public async Task<ExtensionDefination> InstallFromMarketplace(long marketplaceId)
        {
            var marketPlaceItem = _marketplaceItemRepository.GetAll().FirstOrDefault(x => x.Id == marketplaceId);
            var marketplace = _extensionRepository.GetAll().Where(x => x.MarketplaceItemId == marketplaceId).FirstOrDefault();
            if (marketplace != null)
                throw new Exception(""); //;new AlreadyExistException((nameof(MarketplaceItem)), instance: marketplaceId.ToString());


            return await _Install(marketPlaceItem.Defination, marketPlaceItem.Id);
        }

        public async Task<ExtensionDto> GetExtension(long id)
        {
            var extension = _extensionRepository.GetAllIncluding(x => x.Items).FirstOrDefault(x => x.Id == id);
            return _objectMapper.Map<ExtensionDto>(extension);
        }

        public async Task<ExtensionDefination> GetExtensionDefination(long id)
        {
            var extension = _extensionRepository.GetAllIncluding(x => x.Items).FirstOrDefault(x => x.Id == id);
            JArray configuration = null;
            if (!String.IsNullOrEmpty(extension.Configuration))
            {
                extension.Configuration = CommonHelper.Decrypt(extension.Configuration);
                configuration = JArray.Parse(extension.Configuration);
            }
            var defination = _extensionReader.ParseDefination(JToken.Parse(extension.Defination), configuration);
            extension.Configuration = CommonHelper.Encrypt(extension.Configuration);
            return defination;
        }

        public async Task SetConfiguration(ExtensionConfigurationParameter parameter)
        {
            var extension = _extensionRepository.GetAllIncluding(x => x.Items).FirstOrDefault(x => x.Id == parameter.Id);
            extension.Configuration = JsonConvert.SerializeObject(parameter.Configurations);
            extension.Configuration = CommonHelper.Encrypt(extension.Configuration);
            _extensionRepository.Update(extension);
            await currentUnitOfWorkProvider.Current.SaveChangesAsync();
        }

        public async Task<ExtensionDto> ChangeStatus(long id, ExtensionStatus status)
        {
            var extension = _extensionRepository.FirstOrDefault(x => x.Id == id);
            //TODO : Check Extension Installation Rules

            extension.Status = status;

            _extensionRepository.Update(extension);

            return _objectMapper.Map<ExtensionDto>(extension);

        }

        public async Task Uninstall(long id)
        {
            var extension = _extensionRepository.GetAllIncluding(x => x.Items).FirstOrDefault(x => x.Id == id);
            foreach (var item in extension.Items)
            {
                var extensions = _extensionReader.Read(item.Defination);
                extensions.Items.ForEach(x => x.Uninstall(extensions));
                await _extensionItemRepository.DeleteAsync(item);
            }

            await _extensionRepository.DeleteAsync(extension);
        }
    }
}