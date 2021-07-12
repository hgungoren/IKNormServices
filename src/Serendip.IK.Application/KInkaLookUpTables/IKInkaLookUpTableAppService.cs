using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.KInkaLookUpTables.Dto;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Serendip.IK.KInkaLookUpTables
{
    public interface IKInkaLookUpTableAppService : IApplicationService, ITransientDependency
    {
        //Task<List<KInkaLookUpTableDto>> GetTableAsync([NotNull] string key);
    }
}