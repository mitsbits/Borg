using Borg.Framework;
using Borg.Framework.Modularity;
using Borg.System.Backoffice.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Borg.System.Backoffice.Areas.Backoffice.Controllers
{
    public class PlugableServicesController : BackOfficeController
    {
        private readonly IPlugableServiceRegistry _plugableServiceRegistry;

        public PlugableServicesController(ILoggerFactory loggerFactory, IUserSession user, IPlugableServiceRegistry plugableServiceRegistry) : base(loggerFactory, user)
        {
            _plugableServiceRegistry = plugableServiceRegistry;
        }

        public IActionResult Index()
        {
            return View(_plugableServiceRegistry.RegisteredServices());
        }
    }
}