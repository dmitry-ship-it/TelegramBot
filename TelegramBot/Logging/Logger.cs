using Microsoft.Extensions.Logging;

namespace TelegramBot.Logging
{
    public sealed class Logger<T> : ILogger<T>
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        private const string _logFilePath = "logs.log";

        public Logger(LoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;

            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => _loggerConfiguration.LogLevels.ContainsKey(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (_loggerConfiguration.EventId == 0 || _loggerConfiguration.EventId == eventId.Id)
            {
                var originalColor = Console.ForegroundColor;

                // log line pattern
                var log = $"{logLevel} [{DateTime.Now:G}] - [{typeof(T)}]: {formatter(state, exception)}{Environment.NewLine}";

                Console.ForegroundColor = _loggerConfiguration.LogLevels[logLevel];

                // actual write to console and file
                Console.Write(log);
                File.AppendAllText(_logFilePath, log);

                Console.ForegroundColor = originalColor;
            }
        }
    }
}
