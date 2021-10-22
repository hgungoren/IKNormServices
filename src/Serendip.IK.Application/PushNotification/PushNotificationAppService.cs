using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.PushNotification
{
    public class PushNotificationAppService : IPushNotificationAppService
    {
        public IConfiguration Configuration { get; }
        private ILogger<PushNotificationAppService> _logger;
        public PushNotificationAppService(IConfiguration configuration, ILogger<PushNotificationAppService> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendNotification(string to, string title, string body)
        {
            var firebase = Configuration.GetSection("FireBase");

            var serverKey = string.Format("key={0}", firebase["ServerKey"]);
            var senderId = string.Format("id={0}", firebase["SenderId"]);


            var data = new
            {
                to,
                notification = new { title, body }
            };

            var jsonBody = JsonConvert.SerializeObject(data);
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        _logger.Log(LogLevel.Error, $"Error sending notification. Status Code: {result.StatusCode}");
                    }
                }
            }

            return false;
        }
    }

}
