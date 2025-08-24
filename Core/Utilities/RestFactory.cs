using Microsoft.Extensions.Logging;
using RestSharp;

namespace Core.Utilities
{
    public static class RestFactory
    {
        public static RestSharpClient Create(ILogger logger)
        {
            var client = new RestClient();

            return new RestSharpClient(client, logger);
        }
    }
}
