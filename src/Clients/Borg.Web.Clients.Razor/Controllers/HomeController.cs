using Borg.Framework.Cache;
using Borg.Web.Clients.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Borg.Web.Clients.Razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheClient cache;
        public HomeController(ICacheClient cache)
        {
            this.cache = cache;


        }

        public async Task<IActionResult> Index()
        {
            await cache.Set("key", "xxx", TimeSpan.FromMinutes(12));
            var sss = await cache.Get<string>("key");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}