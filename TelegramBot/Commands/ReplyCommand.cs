using Telegram.Bot.Types.InputFiles;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public class ReplyCommand : Command // TODO: Add unit tests
    {
        public override string ReplyMessage => string.Empty;

        public override InputOnlineFile? ReplySticker => GetRandomFile();

        public static bool CheckCondition(string input)
        {
            var tag = Configuration.Instance.BotTag!;

            return input.StartsWith($"{tag} ")
                || string.Equals(input, tag, StringComparison.OrdinalIgnoreCase)
                || (input.StartsWith(tag) && ContinuesWithSpecialChar(input, tag));
        }

        private static bool ContinuesWithSpecialChar(string input, string tag)
        {
            return char.IsPunctuation(input[tag.Length])
                || char.IsControl(input[tag.Length]);
        }

        private static InputOnlineFile GetRandomFile()
        {
            var fileIDs = Configuration.Instance.ReplyConfig!.FileIDs;

            return new(fileIDs[Random.Shared.Next(0, fileIDs.Length)]);
        }
    }
}
