using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Serendip.IK.Extensions.Core
{
    public class HttpActionTypes
    {
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";

        public static HttpMethod Convert(string method)
        {
            switch (method)
            {
                case HttpActionTypes.GET:
                    return HttpMethod.Get;
                case HttpActionTypes.POST:
                    return HttpMethod.Post;
                case HttpActionTypes.PUT:
                    return HttpMethod.Put;
                case HttpActionTypes.DELETE:
                    return HttpMethod.Delete;
            }
            return HttpMethod.Get;
        }
    }

    public class HttpActionResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        public string ResultString { get; set; }
    }
    
    public class HttpActionRequest
    {
        public ExtensionInvocationContext Context { get; set; }

        public object Data { get; set; }
    }

    public class HttpRequestContext
    {
        public string ExtensionId { get; set; }

        public string ExtensionName { get; set; }
        public long? UserId { get; set; }

        public int TenantId { get; set; }
        
        public object Data { get; set; }

        public Dictionary<string,string> Configuration { get; set; }

        public static HttpRequestContext FromAction(HttpActionRequest request)
        {
            return new HttpRequestContext()
            {
                UserId = request.Context.UserId,
                TenantId = request.Context.TenantId,
                Data = request.Data,
                ExtensionId = request.Context.ExtensionDefination.Id.ToString(),
                ExtensionName = request.Context.ExtensionDefination.Name,
                Configuration = request.Context.ExtensionDefination.Configuration.ToDictionary(x=>x.Name,x=>x.Value)
            };
        }
    }
    public class HttpAction
    {
        public string Url { get; set; }

        public string Method { get; set; }

        public Dictionary<string,string> Headers { get; set; }

        public async Task<HttpActionResponse> Send(HttpActionRequest request)
        {
            var client = new HttpClient();
            //TODO : Url Context ile text template'ten geçmeli.
            var httpRequest = new HttpRequestMessage(HttpActionTypes.Convert(Method),new Uri(Url));
            var json = JsonConvert.SerializeObject(HttpRequestContext.FromAction(request));
            httpRequest.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
            if (Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    //TODO Header'larında text template'ten geçmesi lazım
                    httpRequest.Headers.Add(header.Key,header.Value);
                }
            }
            client.Timeout = TimeSpan.FromSeconds(60);
            try
            {
                var response = await client.SendAsync(httpRequest);

                return new HttpActionResponse()
                {
                    StatusCode = response.StatusCode
                };
            }
            catch (Exception e)
            {
                return new HttpActionResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = e.Message
                };
            }
            
        }
    }
}