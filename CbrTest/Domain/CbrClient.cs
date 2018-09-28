using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CbrTest.Domain
{
    public class CbrClient : ICbrClient
    {
        private const string Endpoint = "https://www.cbr-xml-daily.ru/daily_json.js";

        private DailyRate _dailyRateCache;
        private DateTime? _lastResponseTimeStamp;
        private int _lifeTimeInMinutes = 1;

        public async Task<DailyRate> GetDailyRateAsync()
        {
            if (_lastResponseTimeStamp == null ||
                (DateTime.UtcNow - _lastResponseTimeStamp.Value).TotalMinutes > _lifeTimeInMinutes)
            {
                //TODO some exception handling there, for example show old rates from DB

                _dailyRateCache = await GetDailyRateFromResponseAsync();
                _lastResponseTimeStamp = DateTime.UtcNow;
            }

            return _dailyRateCache;
        }

        private async Task<DailyRate> GetDailyRateFromResponseAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(Endpoint);

            var jObject = JObject.Parse(response);
            var timeStamp = jObject.Value<DateTime>("Date");

            return new DailyRate
            {
                TimeStamp = timeStamp.ToUniversalTime(),
                Currencies = jObject
                    .Value<JObject>("Valute").Properties()
                    .Select(c => JsonConvert.DeserializeObject<Currency>(c.Value.ToString()))
                    .ToArray()
            };
        }
    }
}