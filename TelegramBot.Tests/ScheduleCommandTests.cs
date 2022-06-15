using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Tests
{
    internal class ScheduleCommandTests
    {
        [TestCase("<a href=\"/folder/file.xls\">Some xls file</a>")]
        [TestCase("<a href=\"/folder/file.xlsx\">Some xlsx file</a>")]
        [TestCase("<a href=\"/folder/file.xlsx\"></a>")]
        [TestCase("<div><p><a href=\"/folder/file.xls\">Some xls file inside</a></p></div>")]
        [TestCase("<div><p><a href=\"/folder/file.xlsx\">Some xlsx file inside</a></p></div>")]
        [TestCase("<div><p><a href=\"/folder/file.xlsx\"></a></p></div>")]
        public void CreateSchedule_Valid(string html)
        {
            var obj = new ScheduleCommand();
            var createSchedule = typeof(ScheduleCommand).GetMethod("CreateSchedule", BindingFlags.NonPublic | BindingFlags.Static);

            var scheduleLines = ((string)createSchedule!.Invoke(obj, new[] { html })!)
                .Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);

            Assert.That(scheduleLines, Has.Length.AtLeast(2));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("        ")]
        [TestCase("<div></div>")]
        [TestCase("<a></a>")]
        [TestCase("<a>where is the link?</a>")]
        [TestCase("<a href=\"/folder/file.txt\">Some txt file</a>")]
        [TestCase("<link href=\"/folder/file.xlsx\">Link?!??</link>")]
        [TestCase("<link src=\"/folder/file.xlsx\">with src</link>")]
        [TestCase("<a ref=\"/folder/file.xlsx\">typo</a>")]
        [TestCase("<a hrf=\"/folder/file.xlsx\">typo</a>")]
        [TestCase("<ahref=\"/folder/file.xlsx\">typo</a>")]
        [TestCase("<a href=\"https://folder.com/file.xlsx\">full url also invalid</a>")]
        [TestCase("<a> <href=\"/folder/file.xlsx\">tag</a>")]
        [TestCase("<a href=\"/folder/file.xlsx\">closing tag<a>")]
        [TestCase("<a href=\"/folder/file.xlsx\">closing tag 2</an>")]
        [TestCase("<a  href=\"/folder/file.xlsx\">extra space</a>")]
        [TestCase("<a href=\"/folder/file_дфо.xlsx\">дфо is in excetopns</a>")]
        [TestCase("<a href=\"/folder/file_зфпо.xlsx\">зфпо is in also</a>")]
        [TestCase("<a href=\"/folder/file_ДфО.xlsx\">should ignore case</a>")]
        [TestCase("<a href=\"/folder/file_зФпО.xlsx\">should ignore case</a>")]
        public void CreateSchedule_Invalid(string html)
        {
            var obj = new ScheduleCommand();
            var createSchedule = typeof(ScheduleCommand).GetMethod("CreateSchedule", BindingFlags.NonPublic | BindingFlags.Static);

            var scheduleLines = ((string)createSchedule!.Invoke(obj, new[] { html })!)
                .Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);

            Assert.That(scheduleLines, Has.Length.EqualTo(1));
        }
    }
}
