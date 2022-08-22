using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TelegramBot.Configs
{
    /// <summary>
    /// Represents configuration of all bot commands.
    /// </summary>
    [Serializable]
    public sealed class Configuration
    {
        public string BotToken { get; set; } = default!;
        public string BotTag { get; set; } = default!;
        public Dictionary<string, string> OtherCommands { get; set; } = default!;
        public ScheduleConfinguration ScheduleConfig { get; set; } = default!;
        public QuestionConfinguration QuestionConfig { get; set; } = default!;
        public ReplyConfiguration ReplyConfig { get; set; } = default!;

        public static Configuration LoadConfiguration()
        {
            var cfgText = File.ReadAllText("botConfig.json");

            return JsonSerializer.Deserialize<Configuration>(cfgText)!;
        }

        /// <summary>
        /// Reloads configuration from file if 'R' key is pressed.
        /// </summary>
        /// <param name="cancellationToken"></param>
        internal static void WaitForReset(CancellationToken cancellationToken)
        {
            var logger = BotHosting.ServiceProvider.GetRequiredService<ILogger<Configuration>>();

            while (!cancellationToken.IsCancellationRequested)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.R)
                {
                    Reset(LoadConfiguration());

                    logger.LogInformation(
                        message: "Manual {FullObjectName} reset.",
                        args: nameof(Configuration));
                }
            }
        }

        private static void Reset(Configuration newConfiguration)
        {
            var singleton = BotHosting.ServiceProvider.GetRequiredService<Configuration>();

            singleton.BotToken = newConfiguration.BotToken;
            singleton.BotTag = newConfiguration.BotTag;
            singleton.OtherCommands = newConfiguration.OtherCommands; 
            singleton.ScheduleConfig = newConfiguration.ScheduleConfig;
            singleton.QuestionConfig = newConfiguration.QuestionConfig;
            singleton.ReplyConfig = newConfiguration.ReplyConfig;
        }
    }
}
