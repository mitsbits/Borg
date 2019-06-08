using Borg.Framework;
using Borg.System.Backoffice.Core;
using Borg.System.Backoffice.Lib;
using Microsoft.AspNetCore.Mvc;

namespace Borg.System.Backoffice.Areas.Backoffice.Controllers
{
    public class PlugableServicesController : BackOfficeController
    {
        private readonly IPlugableServiceRegistry _plugableServiceRegistry;

        public PlugableServicesController(IPlugableServiceRegistry plugableServiceRegistry)
        {
            _plugableServiceRegistry = plugableServiceRegistry;
        }

        public IActionResult Index()
        {
            return View(_plugableServiceRegistry.RegisteredServices());
        }
    }
}