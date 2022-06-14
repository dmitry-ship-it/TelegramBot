using Telegram.Bot.Types.InputFiles;
using TelegramBot.Configs;

namespace TelegramBot.Commands
{
    /// <summary>
    /// 50% for 'Да' and 50% for 'Нет'.
    /// Commands: <b>/в {}</b>, <b>/вопр {}</b>, <b>/вопрос {}</b>, <b>/q {}</b>, <b>/question {}</b>
    /// </summary>
    public sealed class QuestionCommand : Command
    {
        private readonly string _selectedSticker;

        public QuestionCommand()
        {
            if (Random.Shared.Next(0, 2) == 1)
            {
                _selectedSticker = Configuration.Instance.QuestionConfig!.YesStickerId;
                ReplyMessage = "Да";
            }
            else
            {
                _selectedSticker = Configuration.Instance.QuestionConfig!.NoStickerId;
                ReplyMessage = "Нет";
            }
        }

        public override string ReplyMessage { get; }

        public override InputOnlineFile? ReplySticker => new(_selectedSticker);

        public static bool CheckCondition(string input)
        {
            return Configuration.Instance.QuestionConfig!.Commands.Any(command =>
                       input.ToLower().StartsWith($"{command} "))
                    && input.EndsWith('?');
        }
    }
}
