using System.Threading.Tasks;

namespace CbrTestAspNet.Domain
{
    public interface ICbrClient
    {
        Task<DailyRate> GetDailyRateAsync();
    }
}