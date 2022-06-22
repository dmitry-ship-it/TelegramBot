using Telegram.Bot.Types.InputFiles;

namespace TelegramBot.Commands
{
    public class CommandFromDictionary : Command
    {
        public CommandFromDictionary(string commandKey)
        {
            ReplyMessage = Configs.Configuration.Instance.OtherCommands![commandKey];
        }

        public override string ReplyMessage { get; }

        public override InputOnlineFile? ReplySticker { get; }

        public static bool CheckCondition(string input)
        {
            return Configs.Configuration.Instance.OtherCommands!.Any(pair =>
                string.Equals(pair.Key, input.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
