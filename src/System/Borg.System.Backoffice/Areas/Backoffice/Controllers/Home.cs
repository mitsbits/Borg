using Borg.System.Backoffice.Core;
using Borg.System.Backoffice.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}