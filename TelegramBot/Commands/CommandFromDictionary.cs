using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public sealed class CommandFromDictionary : Command // TODO: add tests
    {
        private static readonly object syncObject = new();
        private static CommandFromDictionary? _instance;

        public static CommandFromDictionary GetInstance(string input)
        {
            if (_instance is null)
            {
                lock (syncObject)
                {
                    if (_instance is null)
                    {
                        _instance = new(input);
                    }
                }
            }

            return _instance;
        }

        private CommandFromDictionary(string input)
        {
            ReplyMessage = Configuration.Instance.OtherCommands![input];
        }

        public override string? ReplyMessage { get; }

        public override InputOnlineFile? ReplySticker { get; }

        public override bool IsMatch(string input)
        {
            return CheckMatching(input);
        }

        public static bool CheckMatching(string input)
        {
            return Configuration.Instance.OtherCommands!.Any(pair =>
                string.Equals(pair.Key, input.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
