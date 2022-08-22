using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Commands.Abstract;
using System.Reflection;

namespace TelegramBot.Tests
{
    internal class CommandTests
    {
        private Configuration _configuration;
        private ICommandFactory _commandFactory;

        [OneTimeSetUp]
        public void SetUp()
        {
            typeof(BotHosting)
                .GetMethod(
                    name: "SetupHost",
                    bindingAttr: BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new[] { Array.Empty<string>() });

            _configuration = BotHosting.ServiceProvider.GetRequiredService<Configuration>();
            _commandFactory = BotHosting.ServiceProvider.GetRequiredService<ICommandFactory>();
        }

        [Test]
        public void Create_Returns_ScheduleCommand()
        {
            var commands = _configuration.ScheduleConfig.Commands;

            for (var i = 0; i < commands.Length; i++)
            {
                Assert.That(
                    actual: _commandFactory.Create(commands[i]),
                    expression: Is.InstanceOf<ScheduleCommand>(),
                    message: "Invalid object type.");
            }
        }

        [Test]
        public void Create_Returns_QuestionCommand()
        {
            var commands = _configuration.QuestionConfig.Commands;

            for (var i = 0; i < commands.Length; i++)
            {
                Assert.That(
                    actual: _commandFactory.Create($"{commands[i]} question ?"),
                    expression: Is.InstanceOf<QuestionCommand>(),
                    message: "Invalid object type.");
            }
        }

        [TestCase("/roll")]
        [TestCase("/roll ")]
        [TestCase("/roll 1")]
        [TestCase("/roll lorem")]
        [TestCase("/roll ?")]
        [TestCase("/roll ok")]
        public void Create_Returns_RollCommand(string command)
        {
            Assert.That(
                actual: _commandFactory.Create(command),
                expression: Is.InstanceOf<RollCommand>(),
                message: "Invalid object type.");
        }

        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase("", ",")]
        [TestCase("", ", hello")]
        [TestCase("", ".")]
        [TestCase("", ". hi")]
        [TestCase("", "?")]
        [TestCase("", "? hey")]
        [TestCase("", "!")]
        [TestCase("", "! jkilodu")]
        [TestCase("", ";")]
        [TestCase("", "; htyjmhy")]
        [TestCase("", ":")]
        [TestCase("", ": oupikjhg")]
        [TestCase("", "\n")]
        [TestCase("", "\r\n")]
        [TestCase("", "\t")]
        [TestCase("", "\0")]
        [TestCase("", " ")]
        [TestCase("", " 1")]
        [TestCase("", " lorem")]
        [TestCase("", " ipsum?")]
        [TestCase("", " .")]
        [TestCase("", " .variant")]
        [TestCase("", " \n")]
        [TestCase("", " \r\n")]
        [TestCase("", " \t")]
        [TestCase("", " \0")]
        [TestCase("@OtherTag, ", "")]
        [TestCase("some message ", "")]
        [TestCase("\n", "")]
        [TestCase("\0", "")]
        [TestCase("", "\n")]
        [TestCase("", "\0")]
        [TestCase("\n", "\n")]
        [TestCase("\0", "\0")]
        [TestCase(".", ".")]
        [TestCase(",", ",")]
        [TestCase(";", ";")]
        public void Create_ValidAddition_Returns_ReplyCommand(string prefix, string suffix)
        {
            Assert.Multiple(() =>
            {
                Assert.That(
                    actual: _commandFactory.Create(prefix + _configuration.BotTag + suffix),
                    expression: Is.InstanceOf<ReplyCommand>(),
                    message: "Invalid object type.");

                Assert.That(
                    actual: _commandFactory.Create(prefix + _configuration.BotTag.ToLower() + suffix),
                    expression: Is.InstanceOf<ReplyCommand>(),
                    message: "Invalid object type.");
            });
        }

        [TestCase("1", "1")]
        [TestCase("1", " ")]
        [TestCase(" ", "1")]
        [TestCase("@Tag", "")]
        [TestCase("ipsum? ", "@Tag")]
        public void Create_ReplyCommand_InvalidAddition_ThrowsArgumentException(string prefix, string suffix)
        {
            Assert.Throws<ArgumentException>(() => _commandFactory.Create(prefix + _configuration.BotTag + suffix));
        }

        [TestCase("!roll")]
        [TestCase("!r")]
        [TestCase("!q")]
        [TestCase("/")]
        [TestCase("/o")]
        [TestCase("/rol")]
        [TestCase("/ras")]
        [TestCase(".r")]
        [TestCase(".roll")]
        [TestCase("/в")]
        [TestCase("/вопр")]
        [TestCase("/вопрос")]
        [TestCase("/q")]
        [TestCase("/question")]
        [TestCase("/в nytjm")]
        [TestCase("/вопр jtyrhgfd")]
        [TestCase("/вопрос jtyr")]
        [TestCase("/q krtgf")]
        [TestCase("/question yutdf")]
        [TestCase("/rolling")]
        [TestCase("/roll1")]
        [TestCase("@BotTag")]
        [TestCase("just message")]
        [TestCase("      ")]
        public void Create_InvalidCommand_ThrowsArgumentException(string command)
        {
            Assert.That(() => _commandFactory.Create(command), Throws.ArgumentException);
        }
    }
}