using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Logging
{
    public sealed class LoggerConfiguration
    {
        private static readonly object syncObject = new();
        private static LoggerConfiguration? _instance;

        private LoggerConfiguration() { }

        public static LoggerConfiguration Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (syncObject)
                    {
                        if (_instance is null)
                        {
                            _instance = new();
                        }
                    }
                }

                return _instance;
            }
        }

        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {
            [LogLevel.Debug] = ConsoleColor.Gray,
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Warning] = ConsoleColor.Yellow,
            [LogLevel.Error] = ConsoleColor.Red,
            [LogLevel.Critical] = ConsoleColor.Magenta
        };
    }
}
