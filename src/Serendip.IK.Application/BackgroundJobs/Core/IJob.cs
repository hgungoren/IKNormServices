using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;

namespace Serendip.IK.BackgroundJobs.Core
{
    public interface IJob<T>
    {
        void Invoke(JobContext<T> context);
    }
}