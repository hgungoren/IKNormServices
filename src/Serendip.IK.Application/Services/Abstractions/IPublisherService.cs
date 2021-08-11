using System.Threading.Tasks;

namespace Serendip.IK.Services.Abstractions
{
    public interface IPublisherService
    {
        Task Publish<TEntity>(string eventName, TEntity entity, bool doRunWorkFlow = true);
    }
}
