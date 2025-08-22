using Microsoft.Extensions.Configuration;

namespace Core.Utilities
{
    public static class ConfigFactory
    {
        private static IConfigurationRoot? testData;
        private static IConfigurationRoot? configuration;

        public static IConfiguration Get()
        {
            if (configuration == null)
            {
                InitializeConfiguration();
            }

            return configuration!;
        }

        // TODO map to a class instead of using IConfiguration directly
        public static IConfiguration GetTestData()
        {
            if (testData == null)
            {
                InitializeTestData();
            }

            return testData!;
        }

        private static void InitializeTestData()
        {
            testData = Initialize("testData.json");
        }

        private static void InitializeConfiguration()
        {
            configuration = Initialize("appsettings.json");
        }

        private static IConfigurationRoot Initialize(string fileName)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(fileName, optional: true, reloadOnChange: true)
                .Build();
        }
    }
}