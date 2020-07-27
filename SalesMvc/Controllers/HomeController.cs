using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesMvc.Models;
using SalesMvc.Models.ViewModels;

namespace SalesMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Segunda chance pro C#";
            ViewData["Me"] = "By: Cesario el macho";
            ViewData["Email"] = "cesario203@outlook.com";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Ola mundo.";

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
}
