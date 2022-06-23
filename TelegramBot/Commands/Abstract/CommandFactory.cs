namespace TelegramBot.Commands.Abstract
{
    public sealed class CommandFactory : ICommandFactory
    {
        private static CommandFactory? _instance;
        public static CommandFactory Instance => _instance ??= new CommandFactory();

        private CommandFactory() { }

        public Command Create(string? input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (ScheduleCommand.Instance.IsMatch(input))
            {
                return ScheduleCommand.Instance;
            }
            else if (QuestionCommand.Instance.IsMatch(input))
            {
                return QuestionCommand.Instance;
            }
            else if (RollCommand.Instance.IsMatch(input))
            {
                return RollCommand.Instance;
            }
            else if (ReplyCommand.Instance.IsMatch(input))
            {
                return ReplyCommand.Instance;
            } // add more commands there
            else if (CommandFromDictionary.CheckMatching(input))
            {
                return CommandFromDictionary.GetInstance(input);
            }

            throw new ArgumentException($"'{input}' is unknown command.");
        }
    }
}
