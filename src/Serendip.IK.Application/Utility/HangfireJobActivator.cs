using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Serendip.IK.Utility
{
    /// <summary>
    /// Default HangFire JobActivator, will creates new instances instead of resolving them from dependency injection services. So this activator resolved given job class from services. 
    /// BE CAREFUL!! 
    /// The job classes mustn't be added as Scoped to resolve successfully.
    /// </summary>
    public class HangfireJobActivator : JobActivator
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerJobActivator"/> class.
        /// </summary>
        /// <param name="serviceProvider">ServiceProvider of IoC.</param>
        public HangfireJobActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        /// <summary>
        /// Resolves requested type from dependency injection via using <see cref="IServiceProvider"/>. If resolving was failed, calls base method.
        /// </summary>
        /// <param name="jobType">Type to resolve.</param>
        /// <returns>Returns resolved type or called from base method.</returns>
        public override object ActivateJob(Type jobType)
        {
            try
            {

                var d = serviceProvider.GetService(jobType) ?? throw new ArgumentNullException("ResolvedType");

                return serviceProvider.GetService(jobType) ?? throw new ArgumentNullException("ResolvedType");
            }
            catch (Exception ex)
            {
                serviceProvider.GetService<ILogger<HangfireJobActivator>>()?.LogError(ex, "[Hangfire] Type cannot be resolved from IoC.");
                return base.ActivateJob(jobType);
            }
        }
    }
}