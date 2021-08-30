using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serendip.IK.Authorization.Roles;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Roles.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Roles
{

    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        #region Constructor
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly ILogger<RoleAppService> _logger;


        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager, ILogger<RoleAppService> logger)
            : base(repository)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._logger = logger;
        }
        #endregion

        #region Create
        // [AbpAuze(PermissionNames.role_create)]
        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }
        #endregion

        #region GetRolesAsync

        //    [AbpAuthorize(PermissionNames.role_view)]
        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }
        #endregion

        #region UpdateAsync
       // [AbpAuthorize(PermissionNames.role_update)] 
        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            try
            {
                ObjectMapper.Map(input, role);
                CheckErrors(await _roleManager.UpdateAsync(role));
                var grantedPermissions = PermissionManager
                    .GetAllPermissions()
                    .Where(p => input.GrantedPermissions.Contains(p.Name)).ToList();

                await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            return MapToEntityDto(role);
        }
        #endregion

        #region DeleteAsync
        //    [AbpAuthorize(PermissionNames.role_delete)] 
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }
        #endregion

        #region GetAllPermissions
        //    [AbpAuthorize(PermissionNames.role_view)]
        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList()
            ));
        }
        #endregion

        #region CreateFilteredQuery
        //    [AbpAuthorize(PermissionNames.role_view)]
        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }
        #endregion

        #region GetEntityByIdAsync
        //     [AbpAuthorize(PermissionNames.role_view)]
        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region ApplySorting
        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }
        #endregion

        #region CheckErrors
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        #endregion

        #region GetRoleForEdit
        //    [AbpAuthorize(PermissionNames.role_update)]
        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
        #endregion
    }
}

