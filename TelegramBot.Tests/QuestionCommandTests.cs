using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TelegramBot.Commands.Abstract;

namespace TelegramBot.Tests
{
    internal class QuestionCommandTests
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
                    c.GetType() == typeof(QuestionCommand));
        }

        [Test]
        public void ReplySticker_IsNotNull()
        {
            Assert.That(_command.ReplySticker, Is.Not.Null);
        }

        [TestCase("/в ?")] // std cases
        [TestCase("/в ok?")]
        [TestCase("/вопр ?")]
        [TestCase("/вопр ok?")]
        [TestCase("/вопрос ?")]
        [TestCase("/вопрос ok?")]
        [TestCase("/q ?")]
        [TestCase("/q ok?")]
        [TestCase("/question ?")]
        [TestCase("/question ok?")]
        [TestCase("/В ?")] // upper and lower mix
        [TestCase("/В ok?")]
        [TestCase("/вОпР ?")]
        [TestCase("/ВоПр ok?")]
        [TestCase("/вОПрОС ?")]
        [TestCase("/ВОпРОс ok?")]
        [TestCase("/Q ?")]
        [TestCase("/Q ok?")]
        [TestCase("/QUEsTIOn ?")]
        [TestCase("/qUEStION ok?")]
        [TestCase("/q ok???")] // other
        [TestCase("/q ok!???")]
        [TestCase("/q ok.?")]
        [TestCase("/q ......?")]
        public void CheckCondition_Returns_True(string input)
        {
            Assert.That(_command.IsMatch(input), Is.True);
        }

        [TestCase("/q")]
        [TestCase("/question?")]
        [TestCase("/в ?.")]
        [TestCase("/que ok?")]
        [TestCase("/вопр\0ok?")]
        [TestCase("/вопр\tok?")]
        [TestCase("/вопр\nok?")]
        [TestCase("/вопр\r\nok?")]
        public void CheckCondition_Returns_False(string input)
        {
            Assert.That(_command.IsMatch(input), Is.False);
        }
    }
}
