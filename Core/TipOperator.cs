using SlippingTip.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SlippingTip.Core
{
    public interface IGetTip
    {
        public void GetAdvice(Action callback);
    }

    internal class TipOperator: IGetTip
    {
        private readonly HttpClient client;
        public TipOperator()
        {
            client = new HttpClient();
        }

        public void GetAdvice(Action callback)
        {
            Console.WriteLine(CommonText.rndPhrase[0]);
            Task task = GetAdviceTask(callback);
        }
        private async Task GetAdviceTask(Action callback)
        {
            string advice = await GetAdviceAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(advice);
            Console.ForegroundColor = Settings.DefaultColor;

            callback?.Invoke();
        }

        private async Task<string> GetAdviceAsync()
        {
            string url = "https://api.adviceslip.com/advice"; //API URL (GET)

            var response = await client.GetAsync(url); //GET

            if (response.IsSuccessStatusCode)
            {
                // Read And Parse JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                //Classic Deserialization
                var jsonObj = JsonSerializer.Deserialize<Dictionary<string, TipObject>>(responseBody);

                string? info = "Ответ на запрос получен!";

                foreach (var kvp in jsonObj)
                {
                    //JSON looks like {"slip": { "id": 10, "advice": "Some advice."}} || kvp.Key is key and = slip. 
                    //_tip.id = kvp.Value.id;
                    //_tip.advice = kvp.Value.advice;

                    info = FormAdvice(kvp.Value.id, kvp.Value.advice);
                }

                return info;
            }
            else
            {
                return string.Format("Error: " + response.StatusCode.ToString());
            }
        }

        private string FormAdvice(int num, string advice)
        {
            return string.Format($"Advice #{num}: {advice}");
        }
    }
}
