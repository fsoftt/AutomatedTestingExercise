using RestSharp;
using Microsoft.Extensions.Logging;

namespace Core.Utilities
{
    public class RestSharpClient
    {
        private string resource = string.Empty;
        private readonly ILogger logger;
        private readonly IRestClient client;

        public RestSharpClient(IRestClient client, ILogger logger)
        {
            this.client = client;
            this.logger = logger;
        }
        
        public RestSharpClient SetEndpoint(string endpoint)
        {
            resource = endpoint;
            logger.LogInformation("Set endpoint to {endpoint}", endpoint);
            return this;
        }

        public RestSharpClient SetMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                logger.LogWarning("Method is null or empty");
                throw new ArgumentException("Method cannot be null or empty", nameof(method));
            }

            resource = $"{resource}{method}";
            logger.LogInformation("Set method to {method}", method);

            return this;
        }

        public async Task<RestResponse> GetAsync()
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
    }
}