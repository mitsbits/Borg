using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Borg.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }
        public IActionResult Index()
        {
            _cache.SetString("foo1", "bary", new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
            var bar = _cache.GetString("foo1");
            return View();
        }
    }
}