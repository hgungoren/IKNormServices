using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.BackgroundJobs.Core
{
    public interface ICronJobManager : ITransientDependency
    {
        void Init();
        void Register<T>(string jobName,string cron)where T:ICronJob;
    }
}
