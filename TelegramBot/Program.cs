using Telegram.Bot.Types.Enums;
using TelegramBot;
using TelegramBot.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot.Commands.Abstract;
using TelegramBot.Commands;

//Services.Init();

//var main = new Thread(async () =>
//{
    await Services.Run(args);

//while (Services.Provider is null) ;

// actual run
//var bot = Services.Provider.GetRequiredService<Bot>();
//    bot.Start();

    // wait for closing
    //await Task.Delay(-1,
    //    cancellationToken: Services.Provider.GetRequiredService<CancellationTokenSource>().Token);
//});

//var com = new Thread(() =>
//{
//    while (Services.Provider is null) ;

//    var cfg = Services.Provider.GetRequiredService<Configuration>();
//    while (true)
//    {
//        if (Console.ReadKey(true).Key == ConsoleKey.R)
//        {
//            cfg.Reset();
//        }
//    }
//});


////main.Start();
////Thread.Sleep(200);
//com.Start();

////main.Join();
//com.Join();
