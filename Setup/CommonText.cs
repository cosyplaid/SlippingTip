using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlippingTip.Setup
{
    internal static class CommonText
    {
        public static void ShowInitText()
        {
            Console.WriteLine("\n--------- SLIP SOME TIP! ---------\n");
            Console.WriteLine("/start - начать");
            Console.WriteLine("/stop - закончить");
            Console.WriteLine("/cancel, /start - отменить операцию");
            Console.WriteLine("/clear - очистить консоль");
            Console.WriteLine("/settings - настройки; /back, /cancel - выход из настроек");
            Console.WriteLine("/exit - закрыть приложение\n");
        }
        public static void ShowErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = Settings.DefaultColor;
        }

        public static string GetFormatedOperator(double value)
        {
            return value < 0 ? $"({value})" : value.ToString();
        }

        public static List<string> rndPhrase = new List<string>()
        {
            "Устанавливаю связь с космосом...\n",
            "Получаю совет...\n",
        };
    }
}
