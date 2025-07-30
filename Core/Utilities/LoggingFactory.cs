using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Core.Utilities
{
    public static class LoggingFactory
    {
        private static ILoggerFactory? loggerFactory;

        private static ILoggerFactory CreateLoggerFactory(string logFilePath = "logs/log.txt")
        {
            if (loggerFactory != null)
            {
                return loggerFactory;
            }

            var configuration = new LoggerConfiguration();
            SetLoggingLevel(configuration);
            configuration.WriteTo.Console();
            configuration.WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day);

            Log.Logger = configuration
                .CreateLogger();

            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            return loggerFactory;
        }

        private static void SetLoggingLevel(LoggerConfiguration configuration)
        {
            IConfiguration configurationService = ConfigFactory.Get();
            string minimumLogLevel = configurationService["Logging:MinimumLevel"]!;
            switch (minimumLogLevel)
            {
                case "Debug":
                    configuration.MinimumLevel.Debug();
                    break;
                case "Information":
                    configuration.MinimumLevel.Information();
                    break;
                case "Warning":
                    configuration.MinimumLevel.Warning();
                    break;
                case "Error":
                    configuration.MinimumLevel.Error();
                    break;
            }
        }

        public static ILogger<T> CreateLogger<T>(string logFilePath = "logs/log.txt")
        {
            ILoggerFactory factory = CreateLoggerFactory(logFilePath);

            return factory.CreateLogger<T>();
        }
    }
}
