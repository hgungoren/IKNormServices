using Abp.Dependency;
using Abp.Runtime.Session;
using Hangfire;

namespace Serendip.IK.BackgroundJobs.Core
{
    public class BackgroundJobManager:IJobManager
    {
        private IAbpSession _abpSession;

        public BackgroundJobManager(IAbpSession abpSession)
        {
            _abpSession = abpSession;
        }
        public void Queue<TInterface,TParameter>(TParameter parameter)where TInterface:IJob<TParameter>
        {
            var context = new JobContext<TParameter>();
            context.UserId = _abpSession.UserId;
            context.TenantId = _abpSession.TenantId;
            context.Data = parameter;
            var job = IocManager.Instance.Resolve<TInterface>();
            BackgroundJob.Enqueue<TInterface>(x => x.Invoke(context));
        }
    }
}