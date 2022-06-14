﻿using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    /// <summary>
    /// Commands: <b>/р</b>, <b>/расп</b>, <b>/расписание</b>, <b>/r</b>, <b>/rasp</b>, <b>/timetable</b>, <b>/schedule</b>
    /// </summary>
    public sealed class ScheduleCommand : Command
    {
        public override string ReplyMessage => GetSchedule();

        public override InputOnlineFile? ReplySticker => null;

        public static bool CheckCondition(string input)
        {
            return Configuration.Instance.ScheduleConfing!.Commands.Any(
                s => string.Equals(s, input, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetSchedule()
        {
            using var client = new HttpClient();
            var html = client.GetStringAsync(Configuration.Instance.ScheduleConfing!.TimeTableUrl).Result;

            return ParseHtml(html);
        }

        private static string ParseHtml(string html)
        {
            var sb = new StringBuilder();

            sb.Append("Расписание:");
            sb.Append(Environment.NewLine);

            foreach (var item in GetMatches(html).Where(item => IsException(item)))
            {
                sb.Append("> ");
                sb.Append(item.Insert(
                    startIndex: item.IndexOf('\"') + 1,
                    value: Configuration.Instance.ScheduleConfing!.VsuByUrl));

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        private static IEnumerable<string> GetMatches(string s)
        {
            return Regex.Matches(s, "<a href=\"/.*.xls.?\">.*</a>").Select(item => item.Value);
        }

        private static bool IsException(string match)
        {
            return !match.Contains("дфо", StringComparison.OrdinalIgnoreCase)
                && !match.Contains("зфпо", StringComparison.OrdinalIgnoreCase);
        }
    }
}