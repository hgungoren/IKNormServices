using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.BackgroundJobs.Core
{
    public interface ISchedulerJobManager : ITransientDependency
    {
        void Queue<TInterface>() where TInterface : ICron;
    }
}
