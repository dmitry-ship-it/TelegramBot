using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    public sealed class CommandFromDictionary : Command // TODO: add tests
    {
        private readonly Configuration _configuration;
        private readonly ILogger<CommandFromDictionary> _logger;

        public CommandFromDictionary(string input ,Configuration configuration, ILogger<CommandFromDictionary> logger)
        {
            _configuration = configuration;
            _logger = logger;

            ReplyMessage = _configuration.OtherCommands![input];
        }

        public override string? ReplyMessage { get; }

        public override InputOnlineFile? ReplySticker { get; }

        public override bool IsMatch(string input)
        {
            return CheckMatching(input);
        }

        public static bool CheckMatching(string input)
        {
            return Services.Provider.GetRequiredService<Configuration>().OtherCommands!.Any(pair =>
                string.Equals(pair.Key, input.Trim(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
