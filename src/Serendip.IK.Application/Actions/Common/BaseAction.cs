using Microsoft.AspNetCore.Mvc;

namespace Serendip.IK.Action.Common
{
    public abstract class BaseAction
    {
        public abstract void Invoke(ActionContext context); 
        public abstract string Name { get; }
    }
}
