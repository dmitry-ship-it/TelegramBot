using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Tests
{
    internal class ConfigurationTests
    {
        private Configuration _configuration;

        [OneTimeSetUp]
        public void SetUp()
        {
            typeof(Services)
                .GetMethod(
                    name: "SetupHost",
                    bindingAttr: BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new[] { Array.Empty<string>() });

            _configuration = Services.Provider.GetRequiredService<Configuration>();
        }

        [Test]
        public void BotToken_IsNotNull()
        {
            Assert.That(_configuration.BotToken, Is.Not.Null);
        }

        [Test]
        public void BotTag_IsNotNull()
        {
            Assert.That(_configuration.BotTag, Is.Not.Null);
        }

        [Test]
        public void OtherCommands_IsNotNull()
        {
            Assert.That(_configuration.OtherCommands, Is.Not.Null);
        }

        [Test]
        public void ScheduleConfing_IsNotNull()
        {
            Assert.That(_configuration.ScheduleConfig, Is.Not.Null);
        }

        [Test]
        public void QuestionConfig_IsNotNull()
        {
            Assert.That(_configuration.QuestionConfig, Is.Not.Null);
        }

        [Test]
        public void ReplyConfig_IsNotNull()
        {
            Assert.That(_configuration.ReplyConfig, Is.Not.Null);
        }
    }
}
