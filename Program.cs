using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TopAcademyBot
{
    public class Payload
    {
        public string application_key { get; set; }
        public object? id_city { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }


    public class Response
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in_refresh { get; set; }
        public int expires_in_access { get; set; }
        public int user_type { get; set; }
        public City_Data city_data { get; set; }
        public string user_role { get; set; }
    }

    public class City_Data
    {
        public int id_city { get; set; }
        public string prefix { get; set; }
        public string translate_key { get; set; }
        public string timezone_name { get; set; }
        public string country_code { get; set; }
        public int market_status { get; set; }
        public string name { get; set; }
    }


    public class Date
    {
        public string date { get; set; }
        public int lesson { get; set; }
        public string started_at { get; set; }
        public string finished_at { get; set; }
        public string teacher_name { get; set; }
        public string subject_name { get; set; }
        public string room_name { get; set; }
    }

    class Program
    {
        private const string BotToken = "8680227565:AAH7zZNo6nxV0OrtDNA7wzU1zzGq5YViYo8";
        private const string AppKey = "6a56a5df2667e65aab73ce76d1dd737f7d1faef9c52e8b8c55ac75f565d8e8a6";
        private const string Username = "Gevor_vz63";
        private const string Password = "IaK4940a";

        static ITelegramBotClient botClient = new TelegramBotClient(BotToken);

        static async Task Main()
        {
            Console.WriteLine("Запуск бота...");

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMe();
            Console.WriteLine($"Бот @{me.Username} запущен. Нажмите Enter для выхода.");
            Console.ReadLine();

            cts.Cancel();
        }


        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { Text: { } messageText } message) return;

            long chatId = message.Chat.Id;


            if (messageText.ToLower() == "/start")
            {
                await botClient.SendMessage(chatId, "Бот активирован. Я пришлю уведомление за час до пары!");
                _ = Task.Run(() => StartNotificationLoop(chatId));
            }
            if (messageText.ToLower() == "/schedule")
            {
                await botClient.SendMessage(chatId, "Получаю расписание на неделю... ⏳");

                string schedule = await GetScheduleAsync();
                await botClient.SendMessage(chatId, schedule);
            }

        }
        static async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add("origin", "https://journal.top-academy.ru");
            client.DefaultRequestHeaders.Add("referer", "https://journal.top-academy.ru");
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/145.0.0.0 Safari/537.36");

            var loginData = new Payload
            {
                application_key = AppKey,
                id_city = null,
                password = Password,
                username = Username
            };

            var authResp = await client.PostAsJsonAsync("https://msapi.top-academy.ru/api/v2/auth/login", loginData);
            authResp.EnsureSuccessStatusCode();

            var authContent = await authResp.Content.ReadFromJsonAsync<Response>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authContent.access_token);

            return client;
        }
        static async Task<string> GetScheduleAsync()
        {
            try
            {
                using var client = await GetAuthenticatedClientAsync();

                DateTime startDate = DateTime.Today;
                string result = "";

                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDate = startDate.AddDays(i);
                    var scheduleResp = await client.GetAsync($"https://msapi.top-academy.ru/api/v2/schedule/operations/get-by-date?date_filter={currentDate:yyyy-MM-dd}");

                    if (!scheduleResp.IsSuccessStatusCode) return "Не удалось получить расписание.";

                    var lessons = await scheduleResp.Content.ReadFromJsonAsync<List<Date>>();

                    if (lessons == null || lessons.Count == 0)
                    {
                        result += $"📅 на {currentDate:dd-MM-yyyy} занятий не найдено.\n";
                    }
                    else
                    {
                        result += $"📅 Расписание на {currentDate:dd-MM-yyyy}:\n\n";
                        foreach (var item in lessons)
                        {
                            result += $"⏰ {item.started_at} - {item.finished_at}\n" +
                                      $"📘 {item.subject_name}\n" +
                                      $"👤 {item.teacher_name}\n" +
                                      $"🚪 Кабинет: {item.room_name}\n\n";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return $"Ошибка системы: {ex.Message}";
            }
        }
        static async Task<List<Date>> GetLessonsForDate(DateTime date)
        {
            using var client = await GetAuthenticatedClientAsync();
            var response = await client.GetAsync($"https://msapi.top-academy.ru{date:yyyy-MM-dd}");

            if (!response.IsSuccessStatusCode) return new List<Date>();

            return await response.Content.ReadFromJsonAsync<List<Date>>() ?? new List<Date>();
        }

        static async Task StartNotificationLoop(long chatId)
        {
            var notifiedLessons = new HashSet<string>();

            while (true)
            {
                try
                {
                    var lessons = await GetLessonsForDate(DateTime.Today);

                    foreach (var lesson in lessons)
                    {
                        string lessonId = $"{DateTime.Today:yyyy-MM-dd}_{lesson.started_at}_{lesson.subject_name}";

                        if (TimeSpan.TryParse(lesson.started_at, out TimeSpan startTimespan))
                        {
                            DateTime startTime = DateTime.Today.Add(startTimespan);
                            var timeUntilStart = startTime - DateTime.Now;

                            if (timeUntilStart.TotalMinutes <= 60 && timeUntilStart.TotalMinutes > 0 && !notifiedLessons.Contains(lessonId))
                            {
                                await botClient.SendMessage(chatId,
                                    $"🔔 Напоминание! Через час начнется занятие:\n" +
                                    $"📘 {lesson.subject_name}\n" +
                                    $"⏰ {lesson.started_at}\n" +
                                    $"🚪 {lesson.room_name}");

                                notifiedLessons.Add(lessonId);
                            }
                        }
                    }

                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute < 10) notifiedLessons.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка] {DateTime.Now}: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }

        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("Ошибка API: " + exception.Message);
            return Task.CompletedTask;
        }
    }
}
