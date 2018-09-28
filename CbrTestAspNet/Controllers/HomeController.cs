using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CbrTestAspNet.Domain;
using CbrTestAspNet.Models;

namespace CbrTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICbrClient _cbrClient;

        public HomeController(ICbrClient cbrClient)
        {
            _cbrClient = cbrClient;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CurrenciesRate()
        {
            var dailyRate = await _cbrClient.GetDailyRateAsync();
            ViewData["Date"] = dailyRate.TimeStamp.ToString("d");
            return View(dailyRate.Currencies.Select(s=> CurrencyViewModel.Create(s.Value)));
        }
    }
}