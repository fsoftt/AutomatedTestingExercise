using Microsoft.Extensions.Configuration;

namespace Core.Utilities
{
    public static class ConfigFactory
    {
        private static IConfigurationRoot? configuration;

        public static IConfiguration Get()
        {
            if (configuration == null)
            {
                Initialize();
            }

            return configuration!;
        }

        private static void Initialize()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}