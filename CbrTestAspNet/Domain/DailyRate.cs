using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CbrTestAspNet.Domain
{
    public class DailyRate
    {
        public DateTime TimeStamp { get; set; }

        [JsonProperty("Valute")]
        public Dictionary<string, Currency> Currencies { get; set; }
    }
}