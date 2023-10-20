using Capsha.WebApplication1.Models;
using Capsha.WebApplication1.Util;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Capsha.WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            (byte[] ms, string c) = Extens.GetCaptchaIMG(true);
            return File(ms, "image/Jpeg");
            //return View();
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