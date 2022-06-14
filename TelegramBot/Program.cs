using Telegram.Bot.Types.Enums;
using TelegramBot;

// actual run
var bot = Bot.GetInstance();
var cts = new CancellationTokenSource();

Console.WriteLine($"Bot started: {bot}");

bot.Start(null, cts.Token);

// wait for closing
await Task.Delay(-1, cancellationToken: cts.Token);

//var c = Command.Create("/р");
//Console.WriteLine(c.ReplyMessage);
