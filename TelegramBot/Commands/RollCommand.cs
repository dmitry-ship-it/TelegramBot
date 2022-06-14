using Telegram.Bot.Types.InputFiles;

namespace TelegramBot.Commands
{
    /// <summary>
    /// Commands: <b>/roll</b>
    /// </summary>
    public class RollCommand : Command
    {
        public override string ReplyMessage => $"Случайное число (1-100):{Environment.NewLine}{Random.Shared.Next(1, 101)}";

        public override InputOnlineFile? ReplySticker => null;

        public static bool CheckCondition(string input)
        {
            return string.Equals(input, "/roll", StringComparison.OrdinalIgnoreCase);
        }
    }
}
