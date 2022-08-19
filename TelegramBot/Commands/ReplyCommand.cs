using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public sealed class ReplyCommand : Command
    {
        private readonly Configuration _configuration;
        private readonly ILogger<ReplyCommand> _logger;

        public ReplyCommand(Configuration configuration, ILogger<ReplyCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public override string ReplyMessage => string.Empty;

        public override InputOnlineFile? ReplySticker => GetRandomFile();

        public override bool IsMatch(string input)
        {
            var tag = _configuration.BotTag!;
            var tagIndex = input.IndexOf(tag, StringComparison.OrdinalIgnoreCase);

            if (tagIndex == -1)
            {
                return false;
            }

            // check chars before and after tag: ',@TagBot?' - True
            return (tagIndex <= 0 || IsSpecialCharOrSpace(input[tagIndex - 1]))
                && (tagIndex + tag.Length >= input.Length || IsSpecialCharOrSpace(input[tagIndex + tag.Length]));
        }

        private static bool IsSpecialCharOrSpace(char c)
        {
            if (c == '@')
            {
                return false;
            }

            return char.IsWhiteSpace(c)
                || char.IsPunctuation(c)
                || char.IsControl(c);
        }

        private InputOnlineFile GetRandomFile()
        {
            var fileIDs = _configuration.ReplyConfig!.FileIDs;

            return new(fileIDs[Random.Shared.Next(0, fileIDs.Length)]);
        }
    }
}
