using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TelegramBot.Commands;
using TelegramBot.Commands.Abstract;
using NLog.Extensions.Logging;
using TelegramBot.Configs;
using System.Text.Json;

namespace TelegramBot
{
    public static class Services
    {
        public static IServiceProvider Provider { get; private set; } = default!;

        public static async Task Run(string[] args)
        {
            var host = SetupHost(args);
            await host.RunAsync();
        }

        private static IHost SetupHost(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(RegisterServices)
                .Build();

            Provider = host.Services;

            return host;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.ClearProviders();
                options.SetMinimumLevel(LogLevel.Information);
                options.AddNLog();
            });

            // add sevices there
            services.AddSingleton<ICommandFactory, CommandFactory>();

            // services.AddSingleton<ICommand, CommandFromDictionary>();
            services.AddSingleton<ICommand, QuestionCommand>();
            services.AddSingleton<ICommand, ReplyCommand>();
            services.AddSingleton<ICommand, RollCommand>();
            services.AddSingleton<ICommand, ScheduleCommand>();

            services.AddSingleton(JsonSerializer.Deserialize<Configuration>(
                File.ReadAllText(Configuration.FilePath))!);

            Thread.Sleep(300);

            services.AddSingleton<CancellationTokenSource>();

            //services.AddSingleton<ITelegramBotClient, TelegramBotClient>(services =>
            //    );

            //services.AddSingleton<Bot>();
            services.AddHostedService<Bot>();
        }
    }
}
