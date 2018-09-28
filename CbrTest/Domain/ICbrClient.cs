using System.Collections.Generic;
using System.Threading.Tasks;

namespace CbrTest.Domain
{
    public interface ICbrClient
    {
        Task<DailyRate> GetDailyRateAsync();
    }
}