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

        [TestCase("/roll")]
        [TestCase("/roll ")]
        [TestCase("/roll gerheh")]
        [TestCase("/roll \n")]
        [TestCase("/roll \r\n")]
        [TestCase("/roll \0")]
        public void CheckCondition_Returns_True(string input)
        {
            Assert.That(RollCommand.CheckCondition(input), Is.True);
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
            Assert.That(RollCommand.CheckCondition(input), Is.False);
        }
    }
}
