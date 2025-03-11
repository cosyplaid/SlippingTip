using SlippingTip.Core;
using SlippingTip.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SlippingTip.Input
{
    internal class ConsoleInput
    {
        public IGetTip tipOperator;

        public ConsoleInput(IGetTip tipOperator)
        {
            this.tipOperator = tipOperator;
            Program.stateUpdate = Update;
        }

        private void Update()
        {
            //Console.WriteLine("State has updated to {0}...", Program.CurrentState);

            if (Program.CurrentState == States.Start)
            {
                Console.Clear();
                CommonText.ShowInitText();
            }

            if (Program.CurrentState == States.Settings)
                Console.WriteLine("\n--------- НАСТРОЙКИ ---------\n");

            Input();
        }

        public void Input()
        {
            string? str;

            switch (Program.CurrentState)
            {
                case States.Start:
                    Console.Write("Введите /start, чтобы начать: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
                case States.WaitForCommands:
                    Console.Write("Введите /tip, чтобы получить бесплатный совет: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input(); //calc.InputOperator(str);
                    break;
                case States.WaitForAnswer:
                    str = Console.ReadLine();
                    Input();
                    break;
                case States.ShowingResult:
                    Console.WriteLine("Resulted");
                    Program.CurrentState = States.WaitForCommands;
                    break;
                case States.Settings:
                    Console.Write("Введите цвет: ");
                    str = Console.ReadLine();
                    if (!CheckCommands(str))
                        Input();
                    break;
            }
        }

        public bool CheckCommands(string str)
        {
            if (Program.CurrentState == States.Settings)
            {
                switch (str)
                {
                    case "/back":
                        Program.CurrentState = Program.LastState;
                        return true;
                    case "white":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "yellow":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "cyan":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "blue":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                    case "magenta":
                        Settings.SetNewColor(str);
                        Update();
                        return true;
                }
            }

            switch (str)
            {
                case "/start":
                    Console.WriteLine();
                    Program.CurrentState = States.WaitForCommands;
                    return true;
                case "/stop":
                    Program.CurrentState = States.Start;
                    return true;
                case "/cancel":
                    Console.WriteLine();
                    if (Program.CurrentState == States.Start) return false;
                    if (Program.CurrentState == States.Settings) Program.CurrentState = Program.LastState;
                    return true;
                case "/tip":
                    Console.WriteLine();
                    if (Program.CurrentState == States.Start) return false;
                    tipOperator.GetAdvice(() => { Program.CurrentState = States.ShowingResult; });
                    Program.CurrentState = States.WaitForAnswer;
                    return true;
                case "/clear":
                    Console.Clear();
                    Update();
                    return true;
                case "/settings":
                    if (Program.CurrentState != States.Settings) Program.LastState = Program.CurrentState;
                    Program.CurrentState = States.Settings;
                    return true;
                case "/help":
                    CommonText.ShowInitText();
                    Update();
                    return true;
                case "/exit":
                    Environment.Exit(0);
                    return true;
                default: return false;
            }
        }
    }
}
