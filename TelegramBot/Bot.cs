using Microsoft.Extensions.Logging;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Commands;
using TelegramBot.Configs;

namespace TelegramBot
{
    public sealed class Bot
    {
        private static Bot? _instance;
        private readonly ITelegramBotClient _bot;
        private readonly ILogger<Bot> _logger;

        private Bot()
        {
            _bot = new TelegramBotClient(Configuration.Instance.BotToken!);
            _logger = Logging.Logger<Bot>.Instance;
        }

        public static Bot Instance => _instance ??= new();

        /// <summary>
        /// Main update handler.
        /// </summary>
        /// <param name="botClient">Telegram bot instance.</param>
        /// <param name="update">Next update.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // update handling
            if (update?.Message?.Chat is null)
            {
                return;
            }

            try
            {
                _logger.LogInformation($"Message from {update.Message.From}. Content: {update.Message.Text ?? "null"}");

                #if DEBUG
                    _logger.LogDebug(JsonSerializer.Serialize(update));
                #endif

                var command = Command.Create(update.Message.Text);

                if (command.ReplySticker is null)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat!,
                        text: command.ReplyMessage,
                        parseMode: ParseMode.Html,
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await botClient.SendStickerAsync(
                        chatId: update.Message.Chat!,
                        sticker: command.ReplySticker,
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                }

                _logger.LogInformation($"Command {command.GetType()} executed successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation($"{ex.GetType()}: {ex.Message}");
            }
            catch (ApiRequestException ex)
            {
                _logger.LogError($"Telegram API request error: {ex.Message}");
            }
        }

        /// <summary>
        /// This method should handle critical errors.
        /// </summary>
        /// <param name="botClient">Telegram bot instance.</param>
        /// <param name="exception">Execption to handle.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // remove creating new task after adding something awaitable there 
            await Task.Factory.StartNew(() =>
            {
                _logger.LogCritical($"{exception.GetType()}: {exception.Message}");
                _logger.LogInformation("Restarting Bot...");
            }, cancellationToken);

            // restart after 1 sec delay
            await Task.Delay(1000, cancellationToken);

            System.Diagnostics.Process.Start("TelegramBot.exe");
            Environment.Exit(1);
        }

        public void Start(Action<ReceiverOptions>? options = null, CancellationToken cancellationToken = default)
        {
            var receiverOptions = new ReceiverOptions();
            options?.Invoke(receiverOptions);

            _logger.LogInformation("Bot started.");
            _bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        }
    }
}
