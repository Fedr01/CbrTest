using System;
using System.Collections.Generic;

namespace CbrTest.Domain
{
    public class DailyRate
    {
        public DateTime TimeStamp { get; set; }

        public IReadOnlyCollection<Currency> Currencies { get; set; }
    }
}