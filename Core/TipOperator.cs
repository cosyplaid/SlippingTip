﻿using SlippingTip.Setup;
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
            Console.WriteLine();
            Console.ForegroundColor = Settings.DefaultColor;

            callback?.Invoke();
        }

        private async Task<string> GetAdviceAsync()
        {
            string url = "https://api.adviceslip.com/advice"; //API URL (GET)

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jsonObj = JsonSerializer.Deserialize<Dictionary<string, TipObject>>(responseBody);

                    string? info = "";

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
                    return string.Format($"Error: {response.StatusCode.ToString()}");
                }
            }
            catch (HttpRequestException e)
            {
                return string.Format($"Error: {e.Message}");
            }
        }

        private string FormAdvice(int num, string advice)
        {
            return string.Format($"Advice #{num}: {advice}");
        }
    }
}
