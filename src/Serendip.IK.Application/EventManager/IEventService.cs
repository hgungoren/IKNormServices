namespace Serendip.IK.EventManager
{
    public interface IEventService
    {
        EventParameter GetEventParameter<TEntity>(string eventName, TEntity entity);
    }
}
