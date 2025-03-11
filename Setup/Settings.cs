using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlippingTip.Setup
{
    internal static class Settings
    {
        static ConsoleColor _defaultColor = ConsoleColor.White;

        public static ConsoleColor DefaultColor { get { return _defaultColor; } }

        public static void SetNewColor(string color)
        {
            switch (color)
            {
                case "white":
                    _defaultColor = ConsoleColor.White;
                    break;
                case "yellow":
                    _defaultColor = ConsoleColor.Yellow;
                    break;
                case "cyan":
                    _defaultColor = ConsoleColor.Cyan;
                    break;
                case "blue":
                    _defaultColor = ConsoleColor.Blue;
                    break;
                case "magenta":
                    _defaultColor = ConsoleColor.Magenta;
                    break;
                default:
                    _defaultColor = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = Settings.DefaultColor;
        }
    }
}
