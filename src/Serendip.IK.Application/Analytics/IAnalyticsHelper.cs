using Abp.Dependency;

namespace Serendip.IK.Analytics
{
    public interface IAnalyticsHelper:ITransientDependency
    {
        string GenerateTrackLink(string link, string id);

        string GetPixelImg(string id,string eventName);
    }
}