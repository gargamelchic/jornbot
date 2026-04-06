using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
using Telegram.Bot.Types.ReplyMarkups;
using tggargamel.Data;
using tggargamel.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TopAcademyBot
{
    
    //Contacts
    public class Contacts
    {
        public Adress[] adress { get; set; }
        public string[] learning_tel { get; set; }
        public Teach_Main[] teach_main { get; set; }
        public string[] href_teach { get; set; }
        public string[] tel_room { get; set; }
        public object tel_book { get; set; }
        public object tel_economists { get; set; }
        public string[] site_shag { get; set; }
        public object mail_shag { get; set; }
        public string[] vk { get; set; }
        public object facebook { get; set; }
        public object instagram { get; set; }
        public object youtube { get; set; }
        public object twitter { get; set; }
        public object google_maps { get; set; }
        public object google_plus { get; set; }
        public object[] teach_contacts { get; set; }
        public object schedule_academic { get; set; }
        public object schedule_office { get; set; }
        public object remote_class_address { get; set; }
        public object happy_manager_contacts { get; set; }
    }

    public class Adress
    {
        public string adress_name { get; set; }
        public string[] learning_tel { get; set; }
        public string[] tel_room { get; set; }
    }

    public class Teach_Main
    {
        public string teachMain_name { get; set; }
        public string[] href_teach { get; set; }
    }
    //Userinfo
    public class Userinfo
    {
        public Group[] groups { get; set; }
        public object manual_link { get; set; }
        public int student_id { get; set; }
        public int current_group_id { get; set; }
        public string full_name { get; set; }
        public int achieves_count { get; set; }
        public int stream_id { get; set; }
        public string stream_name { get; set; }
        public string group_name { get; set; }
        public int level { get; set; }
        public string photo { get; set; }
        public Gaming_Points[] gaming_points { get; set; }
        public Spent_Gaming_Points[] spent_gaming_points { get; set; }
        public Visibility visibility { get; set; }
        public int current_group_status { get; set; }
        public string birthday { get; set; }
        public int age { get; set; }
        public string last_date_visit { get; set; }
        public string registration_date { get; set; }
        public int gender { get; set; }
        public string study_form_short_name { get; set; }
    }

    public class Visibility
    {
        public bool is_design { get; set; }
        public bool is_video_courses { get; set; }
        public bool is_vacancy { get; set; }
        public bool is_signal { get; set; }
        public bool is_promo { get; set; }
        public bool is_test { get; set; }
        public bool is_email_verified { get; set; }
        public bool is_quizzes_expired { get; set; }
        public bool is_debtor { get; set; }
        public bool is_phone_verified { get; set; }
        public bool is_only_profile { get; set; }
        public bool is_referral_program { get; set; }
        public bool is_dz_group_issue { get; set; }
        public bool is_birthday { get; set; }
        public bool is_school { get; set; }
        public bool is_news_popup { get; set; }
        public bool is_school_branch { get; set; }
        public bool is_college_branch { get; set; }
        public bool is_higher_education_branch { get; set; }
        public bool is_russian_branch { get; set; }
        public bool is_academy_branch { get; set; }
    }

    public class Group
    {
        public int group_status { get; set; }
        public bool is_primary { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Gaming_Points
    {
        public int new_gaming_point_types__id { get; set; }
        public int points { get; set; }
    }

    public class Spent_Gaming_Points
    {
        public int new_gaming_point_types__id { get; set; }
        public int points { get; set; }
    }


    //Settings
    public class Settings
    {
        public int id { get; set; }
        public string ful_name { get; set; }
        public string address { get; set; }
        public string date_birth { get; set; }
        public string study { get; set; }
        public string email { get; set; }
        public int last_approving_status { get; set; }
        public int form_type { get; set; }
        public string photo_path { get; set; }
        public bool has_not_approved_data { get; set; }
        public bool has_not_approved_photo { get; set; }
        public bool is_email_verified { get; set; }
        public bool is_phone_verified { get; set; }
        public Phone[] phones { get; set; }
        public Link[] links { get; set; }
        public Relative[] relatives { get; set; }
        public int fill_percentage { get; set; }
        public object decline_comment { get; set; }
        public Azure azure { get; set; }
        public object azure_login { get; set; }
    }

    public class Azure
    {
        public object login { get; set; }
        public bool has_azure { get; set; }
        public bool has_office { get; set; }
    }

    public class Phone
    {
        public int phone_type { get; set; }
        public string phone_number { get; set; }
    }

    public class Link
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reg { get; set; }
        public int required_type { get; set; }
        public object value { get; set; }
        public bool valid { get; set; }
        public bool is_required { get; set; }
        public bool show_link { get; set; }
    }

    public class Relative
    {
        public string full_name { get; set; }
        public string address { get; set; }
        public string relationship { get; set; }
        public Phone[] phones { get; set; }
        public string[] emails { get; set; }
    }

    
    //Payload
    public class Payload
    {
        public string application_key { get; set; }
        public object? id_city { get; set; }
        public string password { get; set; }
        public string username { get; set; }
    }

    //Response
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

    //Date
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
    public class UserSession
    {
        public string Step { get; set; } = "None";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
    class Program
    {
        private const string BotToken = "8680227565:AAH7zZNo6nxV0OrtDNA7wzU1zzGq5YViYo8";
        private const string AppKey = "6a56a5df2667e65aab73ce76d1dd737f7d1faef9c52e8b8c55ac75f565d8e8a6";
        private const long AdminId = 6009797426;

        static ITelegramBotClient botClient = new TelegramBotClient(BotToken);
        static Dictionary<long, UserSession> sessions = new();

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
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMe();
            Console.WriteLine($"Бот @{me.Username} запущен. Нажмите Enter для выхода.");
            Console.ReadLine();

            cts.Cancel();
        }


        private static async Task HandleMessageAsync(Message message, string messageText)
        {

            long chatId = message.Chat.Id;
            if (!sessions.ContainsKey(chatId)) sessions[chatId] = new UserSession();
            UserSession user = sessions[chatId];
            

            if (messageText == "/start")
            {
                using (var db = new AppDbContext())
                {
                    db.Database.EnsureCreated();
                    var student = db.Students.FirstOrDefault(s => s.TgId == chatId);
                    if (student != null)
                    {
                        Console.WriteLine("Студент уже есть в базе!");
                        user.Login = student.Login;
                        user.Password = student.Password;
                        user.Step = "WaitingPass";
                    }
                    else
                    {
                        Console.WriteLine("Студента нет, можно добавлять.");
                        user.Step = "WaitingLogin";
                        await botClient.SendMessage(chatId, "Введите ваш логин:");
                        await botClient.SendMessage(AdminId, "");
                        return;
                    }
                }
            }
            switch (user.Step)
            {
                case "WaitingLogin":
                    user.Login = messageText;
                    user.Step = "WaitingPass";
                    await botClient.SendMessage(chatId, "Принято. Теперь введите пароль:");
                    break;

                case "WaitingPass":
                    if(user.Password == null)
                    {
                        user.Password = messageText;
                        using (var db = new AppDbContext())
                        {
                            db.Database.EnsureCreated();

                            var student = new Student
                            {
                                TgId = chatId,
                                Password = user.Password,
                                Login = user.Login,
                            };

                            db.Students.Add(student);
                            db.SaveChanges();
                            Console.WriteLine("Студент успешно добавлен!");
                        }
                    }
                    else
                    {
                        var menu = new InlineKeyboardMarkup(new[]
                        {
                            new[] { InlineKeyboardButton.WithCallbackData("📅 Расписание на завтра", "schedule") },
                            new[] { InlineKeyboardButton.WithCallbackData("👤 Профиль", "profile") },
                            new[] { InlineKeyboardButton.WithCallbackData("📞 Контакты", "contacts") }
                        });
                        await botClient.SendMessage(chatId, "Вы в основном меню. Выберите функцию...", replyMarkup: menu);
                    }
                    user.Step = "Authorized";
                    break;


                case "Authorized":
                    break;
            }
        }
        static async Task<HttpClient> GetAuthenticatedClientAsync(string Username, string Password)
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
        private static async Task HandleCallbackAsync(CallbackQuery cb)
        {
            long chatId = cb.Message!.Chat.Id;

            if (!sessions.ContainsKey(chatId)) sessions[chatId] = new UserSession();
            UserSession user = sessions[chatId];
            await botClient.AnswerCallbackQuery(cb.Id);

            switch (cb.Data)
            {
                case "schedule":

                    await botClient.SendMessage(chatId, "📅 Расписание на завтра.");

                    string schedule = await GetScheduleAsync(user.Login, user.Password);
                    await botClient.SendMessage(chatId, schedule); 
                    break;

                case "contacts":
                    await botClient.SendMessage(chatId, "📞 Контакты");

                    string contacts = await GetContactsAsync(user.Login, user.Password); 
                    await botClient.SendMessage(chatId, contacts);
                    break;


                case "profile":
                    await botClient.SendMessage(chatId, "👤 Профиль");
                    string profile = await GetProfileAsync(user.Login, user.Password);
                    await botClient.SendMessage(chatId, profile);
                    break;
            }
        }
        static async Task<string> GetProfileAsync(string Username, string Password)
        {
            try
            {
                using var client = await GetAuthenticatedClientAsync(Username, Password);
                string result = "";
                var settings = await client.GetAsync("https://msapi.top-academy.ru/api/v2/profile/operations/settings");
                var info = await client.GetAsync("https://msapi.top-academy.ru/api/v2/settings/user-info");
                if (!settings.IsSuccessStatusCode) return "Не удалось получить контакты академии.";
                Settings? profile = await settings.Content.ReadFromJsonAsync<Settings>();
                Userinfo? profile2 = await info.Content.ReadFromJsonAsync<Userinfo>();
                result += $"👨‍💻 ФИО студента: {profile.ful_name}\n";
                result += $"Топкоины: {profile2.gaming_points[0].points}🪙\n";
                result += $"Топгемы: {profile2.gaming_points[1].points}💎\n";
                result += $"Поток: {profile2.stream_name}\n";
                result += $"Группа: {profile2.group_name}\n";
                result += $"Дата рождения: {profile.date_birth}\n";
                result += $"Почта: {profile.email}\n";
                result += $"Номер телефона: {profile.phones[0].phone_number}\n";
                return result;
            }
            catch (Exception ex)
            {
                return $"Ошибка системы: {ex.Message}";
            }
        }

        static async Task<string> GetContactsAsync(string Username, string Password)
        {
            try
            {
                using var client = await GetAuthenticatedClientAsync(Username, Password);

                string result = "";

                var contactsResp = await client.GetAsync("https://msapi.top-academy.ru/api/v2/contacts/operations/index");

                if (!contactsResp.IsSuccessStatusCode) return "Не удалось получить контакты академии.";

                Contacts? lessons = await contactsResp.Content.ReadFromJsonAsync<Contacts>();
                result += $"Адресса филиала: {lessons.adress[0].adress_name}";
                for (int i = 1; i < lessons.adress.Count(); i++)
                {
                    result += $", {lessons.adress[i].adress_name}";
                }
                result += $"\nНомера учебной части: {lessons.adress[0].learning_tel[0]}";
                for (int i = 1; i < lessons.adress.Count(); i++)
                {
                    for (int j = 1; j < lessons.adress.Count(); j++)
                        result += $", {lessons.adress[i].learning_tel[j]}";
                }
                result += $"\nЛучший МУП: {lessons.teach_main[0].teachMain_name}❤❤❤";
                for (int i = 1; i < lessons.teach_main.Count(); i++)
                {
                    result += $", {lessons.teach_main[i].teachMain_name}-{lessons.teach_main[i].href_teach}";
                }
                result += $"\nСайт: {lessons.site_shag[0]}";
                for (int i = 1; i < lessons.site_shag.Count(); i++)
                {
                    result += $", {lessons.site_shag[i]}";
                }
                result += $"\nVK: {lessons.vk[0]}";
                for (int i = 1; i < lessons.vk.Count(); i++)
                {
                    result += $", {lessons.vk[i]}";
                }
                return result;
            }
            catch (Exception ex)
            {
                return $"Ошибка системы: {ex.Message}";
            }
        }
        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("Ошибка API: " + exception.Message);
            return Task.CompletedTask;
        }
        static async Task<string> GetScheduleAsync(string Username, string Password)
        {
            try
            {
                using var client = await GetAuthenticatedClientAsync(Username, Password);

                DateTime startDate = DateTime.Today.AddDays(1);
                string result = "";

                var scheduleResp = await client.GetAsync($"https://msapi.top-academy.ru/api/v2/schedule/operations/get-by-date?date_filter={startDate:yyyy-MM-dd}");

                if (!scheduleResp.IsSuccessStatusCode) return "Не удалось получить расписание.";

                var lessons = await scheduleResp.Content.ReadFromJsonAsync<List<Date>>();

                if (lessons == null || lessons.Count == 0)
                {
                    result += $"📅 на {startDate:dd-MM-yyyy} занятий не найдено.\n";
                }
                else
                {
                    result += $"Расписание на {startDate:dd-MM-yyyy}:\n\n";
                    foreach (var item in lessons)
                    {
                        result += $"⏰ {item.started_at} - {item.finished_at}\n" +
                                  $"📘 {item.subject_name}\n" +
                                  $"👤 {item.teacher_name}\n" +
                                  $"🚪 Кабинет: {item.room_name}\n\n";
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Ошибка системы: {ex.Message}";
            }
        }
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.CallbackQuery is { } cb)
                {
                    await HandleCallbackAsync(cb);
                    return;
                }

                if (update.Message is { Text: { } messageText } msg)
                {
                    await HandleMessageAsync(msg, messageText);
                }
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }



        }
        //static async Task<List<Date>> GetLessonsForDate(DateTime date)
        //{
        //    using var client = await GetAuthenticatedClientAsync();
        //    var response = await client.GetAsync($"https://msapi.top-academy.ru{date:yyyy-MM-dd}");

        //    if (!response.IsSuccessStatusCode) return new List<Date>();

        //    return await response.Content.ReadFromJsonAsync<List<Date>>() ?? new List<Date>();
        //}

        //static async Task StartNotificationLoop(long chatId)
        //{
        //    var notifiedLessons = new HashSet<string>();

        //    while (true)
        //    {
        //        try
        //        {
        //            var lessons = await GetLessonsForDate(DateTime.Today);

        //            foreach (var lesson in lessons)
        //            {
        //                string lessonId = $"{DateTime.Today:yyyy-MM-dd}_{lesson.started_at}_{lesson.subject_name}";

        //                if (TimeSpan.TryParse(lesson.started_at, out TimeSpan startTimespan))
        //                {
        //                    DateTime startTime = DateTime.Today.Add(startTimespan);
        //                    var timeUntilStart = startTime - DateTime.Now;

        //                    if (timeUntilStart.TotalMinutes <= 60 && timeUntilStart.TotalMinutes > 0 && !notifiedLessons.Contains(lessonId))
        //                    {
        //                        await botClient.SendMessage(chatId,
        //                            $"🔔 Напоминание! Через час начнется занятие:\n" +
        //                            $"📘 {lesson.subject_name}\n" +
        //                            $"⏰ {lesson.started_at}\n" +
        //                            $"🚪 {lesson.room_name}");

        //                        notifiedLessons.Add(lessonId);
        //                    }
        //                }
        //            }

        //            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute < 10) notifiedLessons.Clear();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"[Ошибка] {DateTime.Now}: {ex.Message}");
        //        }

        //        await Task.Delay(TimeSpan.FromMinutes(5));
        //    }
        //}
    }
}
