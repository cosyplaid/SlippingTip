using SlippingTip.Core;
using SlippingTip.Input;
using System.Net;
using System.Text.Json;

namespace SlippingTip
{
    internal class Program
    {
        static States currentState;
        public static Action? stateUpdate;
        public static States CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                //Console.WriteLine("State is changend to {0}", currentState);
                stateUpdate?.Invoke();
            }
        }

        public static States LastState { get; set; } = States.Start;

        static void Main(string[] args)
        {
            ConsoleInput consoleInputs = new ConsoleInput(new TipOperator());
            CurrentState = States.Start;
        }
    }
}
