using RestSharp;
using Microsoft.Extensions.Logging;

namespace Core.Utilities
{
    public class RestSharpClient
    {
        private readonly IRestClient client;
        private readonly ILogger logger;

        public RestSharpClient(IRestClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<RestResponse> GetAsync(string resource)
        {
            logger.LogInformation("GET {Resource}", resource);

            var request = new RestRequest(resource, Method.Get);

            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> PostAsync(string resource, object body)
        {
            logger.LogInformation("POST {Resource} with body: {Body}", resource, body);

            var request = new RestRequest(resource, Method.Post).AddJsonBody(body);

            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> PutAsync(string resource, object body)
        {
            logger.LogInformation("PUT {Resource} with body: {Body}", resource, body);

            var request = new RestRequest(resource, Method.Put).AddJsonBody(body);

            return await client.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteAsync(string resource)
        {
            logger.LogInformation("DELETE {Resource}", resource);

            var request = new RestRequest(resource, Method.Delete);

            return await client.ExecuteAsync(request);
        }
    }
}