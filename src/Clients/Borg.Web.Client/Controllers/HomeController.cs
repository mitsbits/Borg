using Borg.Framework.Dispatch.Contracts;
using Borg.Web.Client.playground;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace Borg.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IDispatcher _dispatcher;

        public HomeController(IDistributedCache cache, IDispatcher dispatcher)
        {
            _cache = cache;
            _dispatcher = dispatcher;
        }

        public async Task<IActionResult> Index()
        {
            _cache.SetString("foo1", "bary", new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
            var bar = _cache.GetString("foo1");

            await _dispatcher.Send(new PingBlindCommand(), cancellationToken: default);
            var result = await _dispatcher.Send<PongResponse, PingCommand>(new PingCommand(), cancellationToken: default);
            _dispatcher.Publish<PingNotification>(new PingNotification()).AnyContext();
            return View();
        }
    }
}