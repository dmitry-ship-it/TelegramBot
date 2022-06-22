using Telegram.Bot.Types.Enums;
using TelegramBot;
using TelegramBot.Configs;

static async Task Run()
{
    // actual run
    var cts = new CancellationTokenSource();

    Bot.Instance.Start(null, cts.Token);

    // wait for closing
    await Task.Delay(-1, cancellationToken: cts.Token);
    Console.ReadLine();
}

start:

try
{
    await Run();
}
catch (Exception ex)
{
    Thread.Sleep(5000);
    goto start;
}
