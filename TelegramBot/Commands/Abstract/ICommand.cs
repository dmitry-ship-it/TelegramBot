using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace TelegramBot.Commands.Abstract
{
    public interface ICommand
    {
        public string? ReplyMessage { get; }

        public InputOnlineFile? ReplySticker { get; }

        public bool IsMatch(string input);
    }
}
