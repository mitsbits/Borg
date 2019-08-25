using Borg.Web.Clients.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Borg.Web.Clients.Razor.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
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

    [Serializable]
    public class test
    {
        public ICollection<kati> katis { get; set; } = new HashSet<kati>();
    }

    [Serializable]
    public class kati
    {
        public string Name { get; set; } = "sdsdffd";
    }
}