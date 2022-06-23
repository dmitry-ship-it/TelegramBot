using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Tests
{
    internal class ConfigurationTests
    {
        [Test]
        public void BotToken_IsNotNull()
        {
            Assert.That(Configuration.Instance.BotToken, Is.Not.Null);
        }

        [Test]
        public void BotTag_IsNotNull()
        {
            Assert.That(Configuration.Instance.BotTag, Is.Not.Null);
        }

        [Test]
        public void OtherCommands_IsNotNull()
        {
            Assert.That(Configuration.Instance.OtherCommands, Is.Not.Null);
        }

        [Test]
        public void ScheduleConfing_IsNotNull()
        {
            Assert.That(Configuration.Instance.ScheduleConfig, Is.Not.Null);
        }

        [Test]
        public void QuestionConfig_IsNotNull()
        {
            Assert.That(Configuration.Instance.QuestionConfig, Is.Not.Null);
        }

        [Test]
        public void ReplyConfig_IsNotNull()
        {
            Assert.That(Configuration.Instance.ReplyConfig, Is.Not.Null);
        }
    }
}
