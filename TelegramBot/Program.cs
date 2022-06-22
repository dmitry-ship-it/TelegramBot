using Telegram.Bot.Types.Enums;
using TelegramBot;
using TelegramBot.Configs;

var main = new Thread(async () =>
{
    // actual run
    var cts = new CancellationTokenSource();

    Bot.Instance.Start(null, cts.Token);

    // wait for closing
    await Task.Delay(-1, cancellationToken: cts.Token);

    //// can be used for commands testing
    //var c = Command.Create("/р");
    //Console.WriteLine(c.ReplyMessage);
});

var com = new Thread(() =>
{
    while (true)
    {
        if (Console.ReadKey(true).Key == ConsoleKey.R)
        {
            Configuration.Reset();
        }
    }
});

main.Start();
com.Start();

main.Join();
com.Join();
