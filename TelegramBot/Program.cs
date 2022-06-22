using Telegram.Bot.Types.Enums;
using TelegramBot;
using TelegramBot.Configs;

async void Start()
{
    // actual run
    var cts = new CancellationTokenSource();

    Bot.Instance.Start(null, cts.Token);

    // wait for closing
    await Task.Delay(-1, cancellationToken: cts.Token);
}

try
{
    Start();
}
catch (IOException)
{
    Thread.Sleep(5000);
    Start();
}
