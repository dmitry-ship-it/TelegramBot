using Microsoft.Extensions.Logging;

namespace TelegramBot.Logging
{
    public class Logger<T> : ILogger<T>
    {
        private readonly Func<LoggerConfiguration> _getCurrentConfig;
        private const string _logFilePath = "logs.log";

        public Logger(Func<LoggerConfiguration> getCurrentConfig)
        {
            _getCurrentConfig = getCurrentConfig;

            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var config = _getCurrentConfig();

            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var originalColor = Console.ForegroundColor;

                // log line pattern
                var log = $"{logLevel} [{DateTime.Now:G}] - [{typeof(T)}]: {formatter(state, exception)}{Environment.NewLine}";

                Console.ForegroundColor = config.LogLevels[logLevel];

                // actual write to console and file
                Console.Write(log);
                File.AppendAllText(_logFilePath, log);

                Console.ForegroundColor = originalColor;
            }
        }
    }
}
