using System.Text.Json;

namespace TelegramBot.Configs
{
    /// <summary>
    /// <para>Represents configuration for the entire bot. This class uses singleton pattern.</para>
    /// Config file content example (JSON):
    /// <code>
    /// {
    ///   "BotToken": "{TOKEN}",
    ///   "BotTag": "@TagOfSomeBot",
    ///   "ScheduleConfing": {
    ///     "WebsiteUrl": "https://example.com",
    ///     "ScheduleUrl": "https://example.com/folder1/subfolder/schedule.html",
    ///     "Commands": [
    ///       "/р", "/расп", "/расписание", "/r", "/rasp", "/timetable", "/schedule"
    ///     ]
    ///   },
    ///   "QuestionConfig": {
    ///     "YesStickerId": "CAACAgIAAx0CRjcaSAADIWKoTpB1fA2ddYyMIOvlfu_4cCKQAAJkEwACbjo5SZxhFowsz9o1JAQ",
    ///     "NoStickerId": "CAACAgIAAx0CRjcaSAADImKoTrhQGp37WvHwJOPjBItICzCdAALvFgACLvA5SbqLv3DmrHzLJAQ",
    ///     "Commands":	[
    ///       "/в", "/вопр", "/вопрос", "/q", "/question"
    ///     ]
    ///   },
    ///   "ReplyConfig": {
    ///     "FileIDs": [
    ///       "CAACAgIAAx0CRjcaSAACASNiqKpidttxq7EPOyMHVYmG7M8bEgACagEAAhZ8aAMFmkeBMge9nCQE",
    ///       "CAACAgIAAx0CRjcaSAACASRiqKttI0CW1Ji4HtfuBJsEgsT_NQACKwADimqUGPXwLlwpdAilJAQ",
    ///       "https://cdn.7tv.app/emote/618302fe8d50b5f26ee7b9bc/4x"
    ///     ]
    ///   }
    /// }
    /// </code>
    /// </summary>
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
