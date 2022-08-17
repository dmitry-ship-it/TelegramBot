using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Commands.Abstract
{
    public interface ICommandFactory
    {
        public Command Create(string input);
    }
}
