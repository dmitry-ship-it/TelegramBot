namespace TelegramBot.Tests
{
    internal class ReplyCommandTests
    {
        [Test]
        public void ReplySticker_IsNotNull()
        {
            Assert.That(ReplyCommand.Instance.ReplySticker, Is.Not.Null);
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
            var tag = Configuration.Instance.BotTag!;

            Assert.That(ReplyCommand.Instance.IsMatch(prefix + tag + suffix), Is.True);
        }

        [TestCase("@","@")]
        [TestCase("@","")]
        [TestCase("","@")]
        [TestCase("bthinyjrogkl","irntmopf")]
        [TestCase("bthinyjrogkl","")]
        [TestCase("", "irntmopf")]
        [TestCase("oimeuy", ", @Tag")]
        [TestCase("@Tag ", "gyvbunijmk")]
        [TestCase("@Tag", " ")]
        [TestCase(" ", "@Tag")]
        [TestCase("1", null)]
        [TestCase(null, "1")]
        public void CheckCondition_Returns_False(string prefix, string suffix)
        {
            var tag = Configuration.Instance.BotTag!;

            Assert.That(ReplyCommand.Instance.IsMatch(prefix + tag + suffix), Is.False);
        }
    }
}
