using Telegram.Bot.Types.Enums;
using TelegramBot;

// actual run
var bot = Bot.GetInstance();
var cts = new CancellationTokenSource();

Console.WriteLine($"Bot started: {bot}");

bot.Start(null, cts.Token);

// wait for closing
await Task.Delay(-1, cancellationToken: cts.Token);

//var cfg = new Configuration();
//cfg.BotToken = "5512285024:AAFWe-_WO6VqUUWrqAA_fT3llV8hAFUl_Rc";
//cfg.ScheduleConfing = new ScheduleCommand.ScheduleConfinguration("https://vsu.by", "https://vsu.by/universitet/fakultety/matematiki-i-it/raspisanie.html");
//cfg.QuestionConfig = new QuestionCommand.QuestionConfinguration("CAACAgIAAx0CRjcaSAADIWKoTpB1fA2ddYyMIOvlfu_4cCKQAAJkEwACbjo5SZxhFowsz9o1JAQ", "CAACAgIAAx0CRjcaSAADImKoTrhQGp37WvHwJOPjBItICzCdAALvFgACLvA5SbqLv3DmrHzLJAQ");

//System.IO.File.WriteAllText("botConfig.json", JsonSerializer.Serialize(cfg, new JsonSerializerOptions() { WriteIndented = true }));

//var c = Command.Create("/р");
//Console.WriteLine(c.ReplyMessage);
