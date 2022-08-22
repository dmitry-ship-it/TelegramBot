using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Commands.Abstract;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    /// <summary>
    /// 50% for 'Да' and 50% for 'Нет'.
    /// Commands: <b>/в {}</b>, <b>/вопр {}</b>, <b>/вопрос {}</b>, <b>/q {}</b>, <b>/question {}</b>
    /// </summary>
    public sealed class QuestionCommand : Command
    {
        private string? _selectedSticker;
        private string? _selectedMessage;
        private readonly object _syncObject = new();

        private readonly Configuration _configuration;
        private readonly ILogger<QuestionCommand> _logger;

        public QuestionCommand(Configuration configuration, ILogger<QuestionCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
            Reroll();
        }

        public override string? ReplyMessage
        {
            get
            {
                Reroll();
                return _selectedMessage;
            }
        }

        public override InputOnlineFile? ReplySticker => new(_selectedSticker!);

        public override bool IsMatch(string input)
        {
            var commands = _configuration.QuestionConfig.Commands;

            return input.EndsWith('?')
                && commands.Any(command =>
                       input.StartsWith($"{command} ", StringComparison.OrdinalIgnoreCase));
        }

        private void Reroll()
        {
            lock (_syncObject)
            {
                if (Random.Shared.Next(0, 2) == 1)
                {
                    _selectedSticker = _configuration.QuestionConfig.YesStickerId;
                    _selectedMessage = "Да";
                }
                else
                {
                    _selectedSticker = _configuration.QuestionConfig.NoStickerId;
                    _selectedMessage = "Нет";
                }
            }

            _logger.LogInformation("Rolled sticker: {StickerId}; Rolled message: {Message}",
                _selectedSticker, _selectedMessage);
        }
    }
}
