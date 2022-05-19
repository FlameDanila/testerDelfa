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
        public static int Counter = 0;

        [Obsolete]
        static async Task Main(string[] args)
        {
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

            try
            {
                if (msg.Text == "cтарт" || msg.Text == "пройти тест заново" || msg.Text == "start" || msg.Text == "/start")
                {
                    Console.WriteLine("Пришло сообщение: " + msg.Text + " от пользователя " + name);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Вы раньше были клиентом Дельфа?", replyMarkup: StartButtons());
                    Counter = 0;
                }
                if (msg.Text == "Нет")
                {
                    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text + " от пользователя " + name);
                    await client.SendTextMessageAsync(msg.Chat.Id, "У нас есть для вас отличное предложение!\nНовым клиентам мы дарим скидку на первые 5 уроков по программированию\nЗаписаться на  занятия можно тут - ссылка");
                    Counter = 1;
                }
                if (msg.Text == "Да")
                {
                    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text + " от пользователя " + name);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Пожалуйста, оцените качество обучения.\nЭто очень поможет нам для улучшения качество занятий", replyMarkup: Question2Buttons());
                    Counter = 1;
                }
                if (msg.Text == "5" || msg.Text == "4" || msg.Text == "3" || msg.Text == "2" || msg.Text == "1")
                {
                    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text + " от пользователя " + name);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Спасибо за ваш отзыв.\nМожете написать, почему вы выбрали именно этот вариант?", replyMarkup: Question3Buttons());
                    Counter = 2;
                }
                if (msg.Text != "" && Counter == 2)
                {
                    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text + " от пользователя " + name);
                    await client.SendTextMessageAsync(msg.Chat.Id, "Спасибо за ваш отзыв.\nСпешим оповестить вас о том, что нашим постоянным клиентам полагается скидка в 20% на следующуй урок", replyMarkup: Question3Buttons());
                    Counter = 3;
                }
            }
            catch (Exception ex)
            { }

            //if (msg.Text.ToLower() == "да2" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 4))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 3");
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 4:\nНравится ли вам учить детей?", replyMarkup: Question3Buttons());
            //    Counter = 3;
            //}
            //if (msg.Text.ToLower() == "нравится" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 4");
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nВы прирождённый преподаватель!😉\nВероятней всего вам подойдёт профессия Младшего воспитателя🏫\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/obrazovaniie/mladshiy-vospitatel/"}", replyMarkup: EmptyAnsver());
            //    Counter = 4;
            //}
            //if (msg.Text.ToLower() == "не нравится" || (msg.Text.ToLower() == "вернуться к прошлому вопросу" && Counter == 5))
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 4");
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 5:\nХотите ли вы преображать людей?", replyMarkup: Question5Buttons());
            //    Counter = 4;
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 5");
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //    Counter = 5;
            //}
            //if (msg.Text.ToLower() == "не хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 5");
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопрос 6:\nЖелаете ли вы управлять персоналом, нанимать новые кадры?", replyMarkup: Question6Buttons());
            //    Counter = 5;
            //}
            //if (msg.Text.ToLower() == "желаю")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "Вопросов пройдено: 6");
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nУ вас сильно выражены лидерские качества!😉\nВероятней всего вам подойдут профессии связанные с административным персоналом" +
            //        $"\nВыберите профессию по-душе:", replyMarkup: Question7Buttons());
            //}
            //if (msg.Text.ToLower() == "секретарь-администратор")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nАлексей Алексеич, к вам посетитель! Спасибо{name}!😉\nВероятней всего вам подойдёт профессия Секретаря-администратора📝\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/professionalnye-otrasli/administrativnyy-personal/sekretar-administrator/"}", replyMarkup: EmptyAnsver());
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
            //if (msg.Text.ToLower() == "хочу")
            //{
            //    Console.WriteLine("Пришел ответ на вопрос: " + msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, $"Успешно!\nМы уверены, что в ваших руках все станут прекрасными!😉\nВероятней всего вам подойдёт профессия Косметик-эстетиста💇\nСсылка на профессию на нашем портале:{"https://delfa72.ru/kursy/it-professii/servis-i-bytovye-uslugi/kosmetik-estetist/"}", replyMarkup: EmptyAnsver());
            //}
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
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Да" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "Нет" } }
                }
            };
        }
        private static IReplyMarkup Question2Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "5" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "4" } },
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "3" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "2" }, new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = "1" } }
                }
            };
        }
        private static IReplyMarkup Question3Buttons()
        {
            return new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new List<List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton>>
                {
                    new List<Telegram.Bot.Types.ReplyMarkups.KeyboardButton> { new Telegram.Bot.Types.ReplyMarkups.KeyboardButton { Text = " " } }
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