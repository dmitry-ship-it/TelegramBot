using System.Text.Json;

namespace TelegramBot.Configs
{
    [Serializable]
    public sealed class Configuration
    {
        public string? BotToken { get; set; }

        public string? BotTag { get; set; }

        public ScheduleConfinguration? ScheduleConfing { get; set; }

        public QuestionConfinguration? QuestionConfig { get; set; }

        public ReplyConfiguration? ReplyConfig { get; set; }

        [NonSerialized]
        private const string _filePath = "botConfig.json";

        [NonSerialized]
        private static Configuration? _instance;

        public static Configuration Instance =>
            _instance ??= JsonSerializer.Deserialize<Configuration>(File.ReadAllText(_filePath))!;
    }
}
