using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;

namespace TelegramBot.Commands
{
    /// <summary>
    /// Commands: <b>/roll</b>
    /// </summary>
    public sealed class RollCommand : Command
    {
        private readonly ILogger<RollCommand> _logger;

        public RollCommand(ILogger<RollCommand> logger)
        {
            _logger = logger;
        }

        public override string ReplyMessage =>
            $"Случайное число (1-100):{Environment.NewLine}{Random.Shared.Next(1, 101)}";

        public override InputOnlineFile? ReplySticker => null;

        public override bool IsMatch(string input)
        {
            return string.Equals(input, "/roll", StringComparison.OrdinalIgnoreCase)
                || input.StartsWith("/roll ", StringComparison.OrdinalIgnoreCase);
        }
    }
}
