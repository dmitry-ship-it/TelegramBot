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
            var tag = Configuration.Instance.BotTag!;
            var tagIndex = input.IndexOf(tag, StringComparison.OrdinalIgnoreCase);

            if (tagIndex == -1)
            {
                return false;
            }

            // check chars before and after tag: ',@TagBot?' - True
            return (tagIndex <= 0 || IsSpecialCharOrSpase(input[tagIndex - 1]))
                && (tagIndex + tag.Length >= input.Length || IsSpecialCharOrSpase(input[tagIndex + tag.Length]));
        }

        private static bool IsSpecialCharOrSpase(char c)
        {
            if (c == '@')
            {
                return false;
            }

            return char.IsWhiteSpace(c)
                || char.IsPunctuation(c)
                || char.IsControl(c);
        }

        private static InputOnlineFile GetRandomFile()
        {
            var fileIDs = Configuration.Instance.ReplyConfig!.FileIDs;

            return new(fileIDs[Random.Shared.Next(0, fileIDs.Length)]);
        }
    }
}
