using Microsoft.AspNetCore.Mvc;

namespace Borg.System.Backoffice.Areas.Backoffice.Controllers
{
    public class HomeController : BackOfficeController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}