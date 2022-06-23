﻿using Telegram.Bot.Types.InputFiles;

namespace TelegramBot.Commands.Abstract
{
    public abstract class Command
    {
        protected Command() { }

        public static CommandFactory Factory => CommandFactory.Instance;

        public abstract string? ReplyMessage { get; }
        public abstract InputOnlineFile? ReplySticker { get; }

        public abstract bool IsMatch(string input);
    }
}
