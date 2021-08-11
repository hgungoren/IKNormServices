namespace Serendip.IK.Analytics
{
    public class AnalyticsHelper : IAnalyticsHelper
    {
        public string GetPixelImg(string id, string eventName)
        {
            //return "";
            return $"<img width='1' height='1' src='http://localhost:7000/track.gif?id={id}&ev={eventName}'/>";
        }

        public string GenerateTrackLink(string link, string id)
        {
            //return link;
            return $"http://localhost:7000/home/redirect?rurl={link}&id=redirect_test&iid={id}";
        }
    }
}