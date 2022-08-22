using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TelegramBot.Configs;

namespace TelegramBot.Commands.Abstract
{
    public sealed class CommandFactory : ICommandFactory
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly Configuration _configuration;
        private readonly ILogger<CommandFactory> _logger;

        public CommandFactory(
            IEnumerable<ICommand> commands,
            Configuration configuration,
            ILogger<CommandFactory> logger)
        {
            _commands = commands;
            _configuration = configuration;
            _logger = logger;
        }

        public ICommand Create(string? input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var selected = _commands.SingleOrDefault(command => command.IsMatch(input));

            if (selected is not null)
            {
                _logger.LogInformation("Command {CommandType} created.", selected.GetType());
                return selected;
            }

            if (CommandFromDictionary.CheckMatching(input))
            {
                _logger.LogInformation("Command {CommandType} created.", typeof(CommandFromDictionary));

                return new CommandFromDictionary(
                    input,
                    _configuration,
                    BotHosting.ServiceProvider.GetRequiredService<ILogger<CommandFromDictionary>>());
            }

            throw new ArgumentException($"'{input}' is unknown command.");
        }
    }
}
