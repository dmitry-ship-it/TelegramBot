using Microsoft.Extensions.Logging;
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
    ///   "OtherCommands": {
    ///     "/info": "Available commands:\n\nSchedule: gets schedule from vsu.by\n'/р', '/расп', '/расписание', '/r', '/rasp', '/timetable', '/schedule'\n\nQuestion: returns a response (Yes/No)\n'/в', '/вопр', '/вопрос', '/q', '/question'\n\nRoll: generates random number (1-100)\n'/roll'\n\nOther commands:\n'/info' - prints this info"
    ///   },
    ///   "ScheduleConfig": {
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

        public Dictionary<string, string>? OtherCommands { get; set; }

        public ScheduleConfinguration? ScheduleConfig { get; set; }

        public QuestionConfinguration? QuestionConfig { get; set; }

        public ReplyConfiguration? ReplyConfig { get; set; }

        [NonSerialized]
        private const string _filePath = "botConfig.json";

        [NonSerialized]
        private static Configuration? _instance;

        [NonSerialized]
        private static readonly object syncObject = new();

        public static Configuration Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (syncObject)
                    {
                        if (_instance is null)
                        {
                            _instance = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(_filePath))!;
                        }
                    }
                }

                return _instance;
            }
        }

        public static void Reset()
        {
            _instance = null;
            Logging.Logger<Configuration>.Instance.LogInformation("Manual {FullObjectName} reset.", nameof(Configuration));
        }
    }
}
