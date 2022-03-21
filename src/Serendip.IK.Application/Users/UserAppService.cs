using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Roles.Dto;
using Serendip.IK.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {

        #region Constructor
        private readonly IAbpSession _abpSession;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly LogInManager _logInManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<User, long> _repository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        public UserAppService(
            IAbpSession abpSession,
            UserManager userManager,
            RoleManager roleManager,
            LogInManager logInManager,
            IRepository<Role> roleRepository,
            IRepository<User, long> repository,
                    IRepository<UserRole, long> userRoleRepository,
            IPasswordHasher<User> passwordHasher)
            : base(repository)
        {
            _abpSession = abpSession;
            _userManager = userManager;
            _roleManager = roleManager;
            _logInManager = logInManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _repository = repository;
            _userRoleRepository = userRoleRepository;
        }
        #endregion

        #region Create
        //[AbpAuthorize(PermissionNames.subitems_user_view_table_create)]
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {

            CheckCreatePermission();
            var user = ObjectMapper.Map<User>(input);
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await _userManager.CreateAsync(user, input.Password));
            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }
            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }
        #endregion

        #region Edit
        //[AbpAuthorize(PermissionNames.subitems_user_view_table_edit)]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);
            input.UserObjId =Convert.ToInt64(user.UserObjId);
            input.CompanyObjId = user.CompanyObjId.Value;
            input.CompanyCode = user.CompanyCode;
            input.CompanyRelationObjId = user.CompanyRelationObjId.Value;
            input.NormalizedTitle = user.NormalizedTitle;

            MapToEntity(input, user);
            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }
        #endregion

        #region Delete
        //[AbpAuthorize(PermissionNames.subitems_user_view_table_delete)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }
        #endregion

        #region Activate
        public async Task Activate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) => { entity.IsActive = true; });
        }
        #endregion

        #region DeActivate
        public async Task DeActivate(EntityDto<long> user)
        {
            await Repository.UpdateAsync(user.Id, async (entity) => entity.IsActive = false);
        }
        #endregion

        #region GetRoles
        //[AbpAuthorize(PermissionNames.items_user_view)]
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }
        #endregion

        #region ChangeLanguage
        //[AbpAuthorize(PermissionNames.user_changelanguage)]
        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        } 
        #endregion

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        #region MapToEntityDto
        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();
            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        } 
        #endregion

        #region User List
        //[AbpAuthorize(PermissionNames.subitems_user_view_table)]
        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }
        #endregion

        #region GetEntityByIdAsync
        // [AbpAuthorize(PermissionNames.items_user_view)]
        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }
        #endregion

        #region ApplySorting
        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }
        #endregion

        #region CheckErrors
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        #endregion

        #region ChangePassword
        //[AbpAuthorize(PermissionNames.user_changepassword)]
        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }
        #endregion

        #region ResetPassword
        //[AbpAuthorize(PermissionNames.user_resetpassword)]
        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }

            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }

            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }
        #endregion

        #region GetAllUsers
        public List<UserDto> GetAllUsers(int tenantId)
        {
            return ObjectMapper.Map<List<UserDto>>(Repository.GetAllList(x => x.TenantId == tenantId));
        }
        #endregion

        #region GetByEmail
        public async Task<UserDto> GetByEmail(string mail)
        {

            var data = Repository.GetAllList();

            if (Repository.GetAllList(x => x.EmailAddress == mail).Count > 0)
            {
                var user = await Repository.GetAllListAsync(x => x.EmailAddress == mail);
                return ObjectMapper.Map<List<UserDto>>(user).FirstOrDefault();

            }

            return default;
        } 
        #endregion

        #region GetFireBaseToken
        public async Task<string> GetFireBaseToken(long userId)
        {
            var user = await Repository.GetAsync(userId);
            var token = user.FirebaseToken;
            return token;
        } 
        #endregion

        #region UpdateRemoveFireBaseToken
        public async Task<bool> UpdateRemoveFireBaseToken(long userId)
        {
            var user = await Repository.GetAsync(userId);
            user.FirebaseToken = null;
            await Repository.UpdateAsync(user);
            return true;
        } 
        #endregion

        #region UpdateFireBaseToken
        public async Task<string> UpdateFireBaseToken(long userId, string token)
        {

            var users = await Repository.GetAllListAsync(user => user.FirebaseToken == token);
            foreach (var currentUser in users)
            {
                currentUser.FirebaseToken = null;
                await Repository.UpdateAsync(currentUser);
            }

            var user = await Repository.GetAsync(userId);
            user.FirebaseToken = token;
            await Repository.UpdateAsync(user);
            return token;
        } 
        #endregion

        #region GetById
        [UnitOfWork]
        public UserDto GetById(long id)
        {
            var user = _repository.GetAll().AsNoTracking().FirstOrDefault(x => x.Id == id);

            var mappingUSer = ObjectMapper.Map<UserDto>(user);

            var RoleNames = (from u in _repository.GetAll().AsNoTracking()
                             join ur in _userRoleRepository.GetAll().AsNoTracking() on u.Id equals ur.UserId into uur
                             from _uur in uur.DefaultIfEmpty()
                             join r in _roleRepository.GetAll().AsNoTracking() on _uur.RoleId equals r.Id into uurr
                             from _uurr in uurr.DefaultIfEmpty()
                             where u.Id == id
                             select new { u, _uurr }
                            ).ToList().Select(x => String.IsNullOrEmpty(x._uurr.Name) == true ? "" : x._uurr.Name).ToList();

            mappingUSer.RoleNames = RoleNames.ToArray();
            return mappingUSer;
        } 
        #endregion


        



    }
}

