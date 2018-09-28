using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CbrTest.Domain;
using Microsoft.AspNetCore.Mvc;
using CbrTest.Models;

namespace CbrTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICbrClient _cbrClient;

        public HomeController(ICbrClient cbrClient)
        {
            _cbrClient = cbrClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>  CurrenciesRate()
        {
            var dailyRate = await _cbrClient.GetDailyRateAsync();
            ViewData["Date"] = dailyRate.TimeStamp.ToString("d");
            return View(dailyRate.Currencies.Select(CurrencyViewModel.Create));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
