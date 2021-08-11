namespace Serendip.IK.Actions
{
    public abstract class BaseAction
    {
        public abstract void Invoke(ActionContext context);

        public abstract string Name { get; }
    }

    public class ActionContext
    {
        public EventParameter EventParameter { get; set; }

        public long? CurrentUserId { get; set; }
        public int? CurrentTenantId { get; set; }

        public object Data { get; set; }
    }
}
