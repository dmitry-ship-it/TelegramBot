using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Tests
{
    internal class RollCommandTests
    {
        [Test]
        public void ReplyMessage_Valid()
        {
            const int rolls = 100;

            for (var i = 0; i < rolls; i++)
            {
                var value = int.Parse(new RollCommand().ReplyMessage.Split(Environment.NewLine).Last());

                Assert.That(value, Is.InRange(1, 100));
            }
        }

        [Test]
        public void ReplySticker_Null()
        {
            const int rolls = 100;

            for (var i = 0; i < rolls; i++)
            {
                Assert.That(new RollCommand().ReplySticker, Is.Null);
            }
        }
    }
}
