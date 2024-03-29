# Telegram bot
This bot was built specifically for personal using.

## Configuration file (botConfig.json)
Name of configuration file is hardcoded.

Config file content example (JSON):

```json
{
  "BotToken": "Token",
  "BotTag": "@Tag",
  "OtherCommands": {
    "/info": "Available commands:\n\nSchedule: gets schedule from vsu.by\n/р, /расп, /расписание, /r, /rasp, /timetable, /schedule\n\nQuestion: returns a response (Yes/No)\n/в, /вопр, /вопрос, /q, /question\n\nRoll: generates random number (1-100)\n/roll\n\nOthercommands:\n/info - prints THIS info"
  },
  "ScheduleConfig": {
    "ScheduleUrl": "https://vsu.by/universitet/fakultety/matematiki-i-it/raspisanie.html",
    "Commands": [
      "/р",
      "/расп",
      "/расписание",
      "/r",
      "/rasp",
      "/timetable",
      "/schedule"
    ]
  },
  "QuestionConfig": {
    "YesStickerId": "CAACAgIAAx0CRjcaSAADIWKoTpB1fA2ddYyMIOvlfu_4cCKQAAJkEwACbjo5SZxhFowsz9o1JAQ",
    "NoStickerId": "CAACAgIAAx0CRjcaSAADImKoTrhQGp37WvHwJOPjBItICzCdAALvFgACLvA5SbqLv3DmrHzLJAQ",
    "Commands": [
      "/в",
      "/вопр",
      "/вопрос",
      "/q",
      "/question"
    ]
  },
  "ReplyConfig": {
    "FileIDs": [
      "CAACAgIAAx0CRjcaSAACASNiqKpidttxq7EPOyMHVYmG7M8bEgACagEAAhZ8aAMFmkeBMge9nCQE",
      "CAACAgIAAx0CRjcaSAACASRiqKttI0CW1Ji4HtfuBJsEgsT_NQACKwADimqUGPXwLlwpdAilJAQ",
      "https://cdn.7tv.app/emote/618302fe8d50b5f26ee7b9bc/4x"
    ]
  }
}
```

## Running application

- Clone repository:
```shell
git clone https://github.com/dmitry-ship-it/TelegramBot.git
```

- Move to main project of solution:
```shell
cd TelegramBot
```

- !! Add configuration file there.
You can create empty file with this command:
```shell
echo > botConfig.json
```

- Run bot in release mode:
```shell
dotnet run -c Release
```

### Tek stack

- .NET 6
- Telegram.Bot Nuget package
- NUnit
- NLog
- DI (Microsoft.Extensions.DependencyInjection)
- Hosting and startup infrastructure (Microsoft.Extensions.Hosting)
