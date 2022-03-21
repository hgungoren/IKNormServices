using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.SKDepartments.Dto;
using Serendip.IK.SKJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKDepartments
{
    public class SKDepartmentsAppService : AsyncCrudAppService<SKDepartments, SKDepartmentDto, long, PagedDepartmentResultRequestDto, CreateDepartmentDto, SKDepartmentDto>, ISKDepartmentsAppService
    {
        public SKDepartmentsAppService(IRepository<SKDepartments, long> repository) : base(repository)
        {
        }

        public async Task<List<SKDepartmentDto>> GetManagerObjId(long departmentObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.DepartmanObjId == departmentObjId);
            var result = ObjectMapper.Map<List<SKDepartmentDto>>(data);
            return result;
        }
    }
}
