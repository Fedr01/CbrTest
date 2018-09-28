using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CbrTestAspNet.Domain
{
    public class CbrClient : ICbrClient
    {
        private const string Endpoint = "https://www.cbr-xml-daily.ru/daily_json.js";

        private DailyRate _dailyRateCache;
        private DateTime? _lastResponseTimeStamp;
        private int _lifeTimeInMinutes = 1;
        private readonly HttpClient _httpClient;

        public CbrClient()
        {
            _httpClient = new HttpClient();
        }

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
            var response = await _httpClient.GetStringAsync(Endpoint);
            return JsonConvert.DeserializeObject<DailyRate>(response);
        }
    }
}