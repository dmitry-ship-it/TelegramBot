using Telegram.Bot.Types.InputFiles;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public class ReplyCommand : Command
    {
        public override string ReplyMessage => string.Empty;

        public override InputOnlineFile? ReplySticker => GetRandomFile();

        public static bool CheckCondition(string input)
        {
            return input.StartsWith($"{Configuration.Instance.BotTag!} ");
        }

        private static InputOnlineFile GetRandomFile()
        {
            var fileIDs = Configuration.Instance.ReplyConfig!.FileIDs;

            return new(fileIDs[Random.Shared.Next(0, fileIDs.Length)]);
        }
    }
}
