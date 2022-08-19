using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using TelegramBot.Commands.Abstract;

namespace TelegramBot.Tests
{
    internal class RollCommandTests
    {
        private ICommand _command;

        [OneTimeSetUp]
        public void SetUp()
        {
            typeof(Services)
                .GetMethod(
                    name: "SetupHost",
                    bindingAttr: BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new[] { Array.Empty<string>() });

            _command = Services.Provider
                .GetServices<ICommand>()
                .Single(c =>
                    c.GetType() == typeof(RollCommand));
        }

        [Test]
        public void ReplyMessage_Valid()
        {
            const int rolls = 100;

            for (var i = 0; i < rolls; i++)
            {
                var rolledValue = int.Parse(_command.ReplyMessage!.Split(Environment.NewLine).Last());

                Assert.That(rolledValue, Is.InRange(1, 100));
            }
        }

        [Test]
        public void ReplySticker_Null()
        {
            const int rolls = 100;

            for (var i = 0; i < rolls; i++)
            {
                Assert.That(_command.ReplySticker, Is.Null);
            }
        }

        [TestCase("/roll")]
        [TestCase("/roll ")]
        [TestCase("/roll gerheh")]
        [TestCase("/roll \n")]
        [TestCase("/roll \r\n")]
        [TestCase("/roll \0")]
        public void CheckCondition_Returns_True(string input)
        {
            Assert.That(_command.IsMatch(input), Is.True);
        }

        [TestCase("/rolls")]
        [TestCase("/roll?")]
        [TestCase("/rol")]
        [TestCase("/rol1")]
        [TestCase("!roll")]
        [TestCase(".roll")]
        [TestCase("roll")]
        public void CheckCondition_Returns_False(string input)
        {
            Assert.That(_command.IsMatch(input), Is.False);
        }
    }
}
