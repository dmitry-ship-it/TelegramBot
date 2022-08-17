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
        private static QuestionCommand? _instance;

        private string? _selectedSticker;
        private string? _selectedMessage;
        private static readonly object syncObject = new();

        private QuestionCommand()
        {
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

        public static QuestionCommand Instance
        {
            get
            {
                if (_instance is null)
                {
                    lock (syncObject)
                    {
                        if (_instance is null)
                        {
                            _instance = new();
                        }
                    }
                }

                return _instance;
            }
        }

        public override bool IsMatch(string input)
        {
            return Configuration.Instance.QuestionConfig!.Commands.Any(command =>
                       input.StartsWith($"{command} ", StringComparison.OrdinalIgnoreCase))
                    && input.EndsWith('?');
        }

        private void Reroll()
        {
            lock (syncObject)
            {
                if (Random.Shared.Next(0, 2) == 1)
                {
                    _selectedSticker = Configuration.Instance.QuestionConfig!.YesStickerId;
                    _selectedMessage = "Да";
                }
                else
                {
                    _selectedSticker = Configuration.Instance.QuestionConfig!.NoStickerId;
                    _selectedMessage = "Нет";
                }
            }
        }
    }
}
