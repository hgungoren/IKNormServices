using Abp.Application.Services;
using Serendip.IK.SKDepartments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKDepartments
{
    public interface ISKDepartmentsAppService : IAsyncCrudAppService<SKDepartmentDto, long, PagedDepartmentResultRequestDto, CreateDepartmentDto, SKDepartmentDto>
    {
        Task<List<SKDepartmentDto>> GetManagerObjId(long departmentObjId);
    }
}
