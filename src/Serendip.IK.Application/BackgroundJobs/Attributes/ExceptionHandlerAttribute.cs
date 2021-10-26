using Abp.Json;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Hangfire.Storage;
using Refit;

namespace Serendip.IK.BackgroundJobs.Attributes
{
    public class ExceptionHandlerAttribute : JobFilterAttribute, IClientFilter, IServerFilter, IElectStateFilter, IApplyStateFilter
    {
        public void OnCreated(CreatedContext filterContext)
        {
            
        }

        public void OnCreating(CreatingContext filterContext)
        {
            
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if(filterContext.Exception != null)
            {
                //var teamsRestService = RestService.For<ITeamsService>("https://bilgeadamonline.webhook.office.com/webhookb2/3a02d816-ce8d-460d-bad1-4c2fb9ad2baf@9dc02fd4-ef4d-4d58-822e-687cf3e1badb/IncomingWebhook/33226cb523a64a7c90af5b29324831a6/a46915ca-b8f0-4829-8c5a-c7000b81b31e");
                //var requestBody = new TeamsRequestDto
                //{
                //    Text = "There's a bug on Hangfire guys \n" + "Method Name : " + filterContext.BackgroundJob.Job.Method.Name + "\n" + "Exception Details : "+ filterContext.Exception?.InnerException?.Message ?? filterContext.Exception.Message,
                //};
                //var requestBodyJson = requestBody.ToJsonString();
                //teamsRestService.SendNotification(requestBody);
            }
        }

        public void OnPerforming(PerformingContext filterContext)
        {
            
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {

        }

        public void OnStateElection(ElectStateContext context)
        {

        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            
        }
    }
}
