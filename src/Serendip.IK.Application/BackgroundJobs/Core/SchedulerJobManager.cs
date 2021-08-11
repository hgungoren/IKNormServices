using Abp.Dependency;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.BackgroundJobs.Core
{
    public class SchedulerJobManager : ISchedulerJobManager
    {
        public void Queue<TInterface>() where TInterface : ICron
        {
            var job = IocManager.Instance.Resolve<TInterface>();
            BackgroundJob.Enqueue(() => job.Invoke());
        }
    }
}
