using Telegram.Bot.Types.Enums;
using TelegramBot;
using TelegramBot.Configs;

var main = new Thread(async () =>
{
    var cts = new CancellationTokenSource();

    // actual run
    Bot.Instance.Start(null, cts.Token);

    // wait for closing
    await Task.Delay(-1, cancellationToken: cts.Token);
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
