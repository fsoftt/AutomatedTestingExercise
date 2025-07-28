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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            return loggerFactory;
        }

        public static ILogger<T> CreateLogger<T>(string logFilePath = "logs/log.txt")
        {
            ILoggerFactory factory = CreateLoggerFactory(logFilePath);

            return factory.CreateLogger<T>();
        }
    }
}
