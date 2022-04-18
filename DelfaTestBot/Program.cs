using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TL;

namespace DelfaTestBot
{
    class Program
    {
        private static string token { get; set; } = "5183249647:AAHCx42xlNoIEZ51EXA2qo0lJe0e4mp_J4M";
        private static TelegramBotClient client;
        public static int Counter = 1;
        public static string chatId = "";
        public static string VladId = "";
        public static long chatMe = 0;
        public static long chatVlad = 0;
        public static int f = 0;
        public static int l = 0;

        [Obsolete]
        static async Task Main(string[] args)
        {
            static string Config(string what)
            {
                switch (what)
                {
                    case "api_id": return "17257489";
                    case "api_hash": return "1c8608e262882c49cb57d1640b46b559";
                    case "phone_number": return "+79504951460";
                    case "verification_code": Console.Write("Code: "); return Console.ReadLine();
                    case "first_name": return "Danila";      // if sign-up is required
                    case "last_name": return ".";        // if sign-up is required
                    case "password": return "secret!";     // if user has enabled 2FA
                    default: return null;                  // let WTelegramClient decide the default config
                }
            }

            using var wTLClient = new WTelegram.Client(Config);
            var my = await wTLClient.LoginUserIfNeeded();
            Console.WriteLine($"We are logged-in as {my.username ?? my.first_name + " " + my.last_name} (id {my.id})");
            var resolved = await wTLClient.Contacts_ResolveUsername("Flame_chanel"); // username without the @
            var Vlad = await wTLClient.Contacts_ResolveUsername("Vl_ad_fayst");
            chatId = resolved.User.username;
            VladId = Vlad.User.username;
            //await wTLClient.SendMessageAsync(resolved, "Привет, это сообщение отправляется ботом для тестирования его возможностей. Когда будем тестировать его вместе?) Я в 306 кабинете.");
            var tl = wTLClient.Messages_GetAllChats();
            Console.WriteLine(tl);
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            var name = msg.From.FirstName + " " + msg.From.LastName;
            Console.WriteLine(name);

            if (chatMe == e.Message.Chat.Id || e.Message.Chat.Username == chatId)
            {
                chatMe = e.Message.Chat.Id;
                f = 1;
            }
            else
            {
                chatVlad = e.Message.Chat.Id;
                l = 1;
            }

            if (msg.Chat.Username == chatId && l == 1)
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(chatVlad, msg.Text);
            }
            else if (msg.Chat.Username == VladId && f == 1)
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(chatMe, msg.Text);
            }
            //if (msg.Text.ToLower() == "больше или ровно 18" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 2))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 1\n(Количество вопросов может меняться в зависимости от ответов)");
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 2:\nКакие науки вы предпочитаете больше: Гуманитарные или Точные(Технарские)?", replyMarkup: Question2Buttons());
            //    Counter = 1;
            //}
            if (msg.Text.ToLower() == "меньше 18")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 1\n(Количество вопросов может меняться в зависимости от ответов)");
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 2:\nКакие науки вы предпочитаете больше: Гуманитарные или Точные(Технарские)?", replyMarkup: Question2Buttons());
                Counter = 1;
            }
            if (msg.Text.ToLower() == "гуманитарные" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 3))
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 2");
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 3:\nВы любите работать с людьми?", replyMarkup: Question4Buttons());
                Counter = 2;
            }
            if (msg.Text.ToLower() == "да" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 4))
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 3");
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 4:\nНравится ли вам учить детей?", replyMarkup: Question3Buttons());
                Counter = 3;
            }
            if (msg.Text.ToLower() == "нравится" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 4");
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nВы прирождённый преподаватель!😉\nВероятней всего вам подойдёт профессия Младшего воспитателя🏫\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/obrazovaniie/mladshiy-vospitatel/"}", replyMarkup: EmptyAnsver());
                Counter = 4;
            }
            if (msg.Text.ToLower() == "не нравится" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 4");
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 5:\nХотите ли вы преображать людей?", replyMarkup: Question5Buttons());
                Counter = 4;
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 5");
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
                Counter = 5;
            }
            if (msg.Text.ToLower() == "не хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 5");
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 6:\nЖелаете ли вы управлять персоналом, нанимать новые кадры?", replyMarkup: Question6Buttons());
                Counter = 5;
            }
            if (msg.Text.ToLower() == "желаю")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 6");
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nУ вас сильно выражены лидерские качества!😉\nВероятней всего вам подойдут профессии связанные с административным персоналом" +
                    $"\nВыберите профессию по-душе:", replyMarkup: Question7Buttons());
            }
            if (msg.Text.ToLower() == "секретарь-администратор")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nАлексей Алексеич, к вам посетитель! Спасибо{name}!😉\nВероятней всего вам подойдёт профессия Секретаря-администратора📝\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/professionalnye-otrasli/administrativnyy-personal/sekretar-administrator/"}", replyMarkup: EmptyAnsver());
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            }
            if (msg.Text.ToLower() == "хочу")
            {
                Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            }
        }

        private static IReplyMarkup StartButtons()
        {
            //WebClient web = new WebClient();
            //Byte[] Data = web.DownloadData(""); //Загрузка страницы для вывода кнопок с сайта в бота

            //using (FileStream file = new FileStream(@"C:\Users\Public\Music\t.txt", FileMode.Create))
            //{
            //    Byte[] vs = Data;

            //    file.Write(vs, 0, vs.Length);
            //}

            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Преподаватель" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Ученик" } }
                    //new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Больше или ровно 18" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Меньше 18" } }
                }
            };
        }
        private static IReplyMarkup Question2Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Гуманитарные" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Точные" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" } }
                }
            };
        }
        private static IReplyMarkup Question3Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Нравится" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Не нравится" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" } }
                }
            };
        }
        private static IReplyMarkup Question4Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Да" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Нет" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" } }
                }
            };
        }
        private static IReplyMarkup EmptyAnsver()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Пройти тест заново" } }
                }
            };
        }
        private static IReplyMarkup Question5Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Хочу" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Не хочу" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" } }
                }
            };
        }
        private static IReplyMarkup Question6Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Желаю" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Не желаю" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Вернуться к прошлому вопросу" } }
                }
            };
        }
        private static IReplyMarkup Question7Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Секретарь-администратор" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Офис-менеджер" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Архивариус" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Делопроизводитель" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Специалист по кадровому делопроизводству" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Рекрутер" } }
                }
            };
        }
    }
}
