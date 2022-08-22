using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TelegramBot.Commands.Abstract;

namespace TelegramBot.Tests
{
    internal class ReplyCommandTests
    {
        private Configuration _configuration;
        private ICommand _command;

        [OneTimeSetUp]
        public void SetUp()
        {
            typeof(BotHosting)
                .GetMethod(
                    name: "SetupHost",
                    bindingAttr: BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new[] { Array.Empty<string>() });

            _configuration = BotHosting.ServiceProvider.GetRequiredService<Configuration>();
            _command = BotHosting.ServiceProvider
                .GetServices<ICommand>()
                .Single(c =>
                    c.GetType() == typeof(ReplyCommand));
        }

        [Test]
        public void ReplySticker_IsNotNull()
        {
            Assert.That(_command.ReplySticker, Is.Not.Null);
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase("\n", "")]
        [TestCase("\0", "")]
        [TestCase("", "\n")]
        [TestCase("", "\0")]
        [TestCase("\n", "\n")]
        [TestCase("\0", "\0")]
        [TestCase(".", ".")]
        [TestCase(",", ",")]
        [TestCase(";", ";")]
        [TestCase("", ", @Tag")]
        [TestCase("@Tag, ", "")]
        [TestCase("@Tag, ", ", @Tag")]
        [TestCase("some message,", ",message")]
        public void CheckCondition_Returns_True(string prefix, string suffix)
        {
            var tag = _configuration.BotTag;

            Assert.That(_command.IsMatch(prefix + tag + suffix), Is.True);
        }

        [TestCase("@", "@")]
        [TestCase("@", "")]
        [TestCase("", "@")]
        [TestCase("bthinyjrogkl", "irntmopf")]
        [TestCase("bthinyjrogkl", "")]
        [TestCase("", "irntmopf")]
        [TestCase("oimeuy", ", @Tag")]
        [TestCase("@Tag ", "gyvbunijmk")]
        [TestCase("@Tag", " ")]
        [TestCase(" ", "@Tag")]
        [TestCase("1", null)]
        [TestCase(null, "1")]
        public void CheckCondition_Returns_False(string prefix, string suffix)
        {
            var tag = _configuration.BotTag;

            Assert.That(_command.IsMatch(prefix + tag + suffix), Is.False);
        }
    }
}
