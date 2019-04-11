
using Borg.System.Backoffice.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Areas.Backoffice.Controllers
{
    public class HomeController : BackOfficeController
    {

        private readonly IDistributedCache cache;
        public HomeController(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public async Task< IActionResult> Index()
        {
            await cache.SetAsync("a key 2", "koko4".ToBytes(), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
            var data = await cache.GetAsync("a key");
            return View();
        }
    }
}