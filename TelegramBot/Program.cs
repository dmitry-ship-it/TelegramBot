using Telegram.Bot.Types.Enums;
using TelegramBot;

// actual run
var cts = new CancellationTokenSource();
Console.WriteLine("Bot started.");

Bot.Instance.Start(null, cts.Token);

// wait for closing
await Task.Delay(-1, cancellationToken: cts.Token);

//// can be used for commands testing
//var c = Command.Create("/р");
//Console.WriteLine(c.ReplyMessage);
