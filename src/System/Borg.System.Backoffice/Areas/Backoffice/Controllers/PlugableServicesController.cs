using Borg.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

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
