using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    /// <summary>
    /// Commands: <b>/р</b>, <b>/расп</b>, <b>/расписание</b>, <b>/r</b>, <b>/rasp</b>, <b>/timetable</b>, <b>/schedule</b>
    /// </summary>
    public sealed class ScheduleCommand : Command
    {
        private readonly Configuration _configuration;
        private readonly ILogger<ScheduleCommand> _logger;

        public ScheduleCommand(Configuration configuration, ILogger<ScheduleCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public override string ReplyMessage => GetSchedule();

        public override InputOnlineFile? ReplySticker => null;

        private string GetSchedule()
        {
            using var client = new HttpClient();
            var html = client.GetStringAsync(_configuration.ScheduleConfig.ScheduleUrl).Result;

            return CreateSchedule(html);
        }

        private string CreateSchedule(string html)
        {
            var sb = new StringBuilder();

            sb.Append("Расписание:");
            sb.Append(Environment.NewLine);

            foreach (var item in GetMatches(html))
            {
                sb.Append("> ");
                sb.Append(item.Insert(
                    startIndex: item.IndexOf('\"') + 1,
                    value: _configuration.ScheduleConfig.ScheduleUrl.Host));

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public override bool IsMatch(string input)
        {
            var commands = _configuration.ScheduleConfig.Commands;

            return commands.Any(command =>
                string.Equals(command, input, StringComparison.OrdinalIgnoreCase));
        }

        private static IEnumerable<string> GetMatches(string s)
        {
            return Regex.Matches(s, "<a href=\"/.*\\.xls.?\">.*</a>")
                .Select(item => item.Value)
                .Where(s => !IsException(s));
        }

        private static bool IsException(string match)
        {
            return match.Contains("дфо", StringComparison.OrdinalIgnoreCase)
                || match.Contains("зфпо", StringComparison.OrdinalIgnoreCase);
        }
    }
}
