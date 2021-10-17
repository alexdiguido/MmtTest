using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CustomerOrders.Helpers
{
    public class ApiGateway : IApiGateway
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiGateway(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string serviceName, string url, IList<string> pathParameters)
        {
            string uri = url;
            if (pathParameters != null)
            {
                uri += string.Concat(pathParameters.Select(p => $"/{p}"));
            }
            var httpClient = _httpClientFactory.CreateClient(serviceName);
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }

}
