using Telegram.Bot.Types.InputFiles;

namespace TelegramBot.Commands
{
    public abstract class Command
    {
        protected Command() { }

        public static Command Create(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException($"'{s}' is unknown command.");
            }

            if (ScheduleCommand.CheckCondition(s))
            {
                return new ScheduleCommand();
            }
            else if (QuestionCommand.CheckCondition(s))
            {
                return new QuestionCommand();
            }
            else if (RollCommand.CheckCondition(s))
            {
                return new RollCommand();
            }
            else if (ReplyCommand.CheckCondition(s))
            {
                return new ReplyCommand();
            }   // add more commands there
            else
            {
                throw new ArgumentException($"'{s}' is unknown command.");
            }
        }

        public abstract string ReplyMessage { get; }
        public abstract InputOnlineFile? ReplySticker { get; }
    }
}
