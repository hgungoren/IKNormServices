namespace Serendip.IK.ApiResults
{
    public class ApiResult<TData, TMeta>
    { 
        public bool Success { get; set; }
        public virtual string Key { get; set; }
        public virtual TData Data { get; set; }
        public virtual TMeta Meta { get; set; }
        public virtual string Message { get; set; }
        public virtual bool? IsUserFriendlyMessage { get; set; }
    }
     

    public class ApiResult<T> : ApiResult<T, object>
    { 
    }
}
