using Microsoft.Extensions.Logging;

namespace TelegramBot.Logging
{
    public class Logger<T> : ILogger<T>
    {
        private readonly LoggerConfiguration _loggerConfig = LoggerConfiguration.Instance;
        private const string _logFilePath = "logs.log";

        public Logger()
        {
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => _loggerConfig.LogLevels.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_loggerConfig.EventId == 0 || _loggerConfig.EventId == eventId.Id)
            {
                var originalColor = Console.ForegroundColor;

                // log line pattern
                var log = $"{logLevel} [{DateTime.Now:G}] - [{typeof(T)}]: {formatter(state, exception)}{Environment.NewLine}";

                Console.ForegroundColor = _loggerConfig.LogLevels[logLevel];

                // actual write to console and file
                Console.Write(log);

                // TODO: enable later
                // File.AppendAllText(_logFilePath, log);

                Console.ForegroundColor = originalColor;
            }
        }
    }
}
