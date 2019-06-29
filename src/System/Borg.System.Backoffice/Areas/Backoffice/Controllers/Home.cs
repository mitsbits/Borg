using Borg.Framework.Modularity;
using Borg.System.Backoffice.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Borg.System.Backoffice.Areas.Backoffice.Controllers
{
    public class HomeController : BackOfficeController
    {
        private readonly IDistributedCache cache;

        public HomeController(ILoggerFactory loggerFactory, IUserSession user, IDistributedCache cache) : base(loggerFactory, user)
        {
            this.cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}