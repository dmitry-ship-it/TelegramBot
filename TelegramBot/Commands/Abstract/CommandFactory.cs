using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TelegramBot.Configs;

namespace TelegramBot.Commands.Abstract
{
    public sealed class CommandFactory : ICommandFactory
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly Configuration _configuration;

        public CommandFactory(IEnumerable<ICommand> commands, Configuration configuration)
        {
            _commands = commands;
            _configuration = configuration;
        }

        public ICommand Create(string? input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var selected = _commands.SingleOrDefault(command => command.IsMatch(input));

            if (selected is not null)
            {
                return selected;
            }

            if (CommandFromDictionary.CheckMatching(input))
            {
                return new CommandFromDictionary(input, _configuration, Services.Provider.GetRequiredService<ILogger<CommandFromDictionary>>());
            }

            throw new ArgumentException($"'{input}' is unknown command.");
        }
    }
}
