using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;

namespace Serendip.IK.BackgroundJobs.Core
{
    public interface IJobManager:ITransientDependency
    {
        void Queue<TInterface,TParameter>(TParameter parameter)where TInterface:IJob<TParameter>;
    }
}