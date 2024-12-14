using AgriculturalSupplyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgriculturalSupplyStore.Controllers
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

		[Route("/404")]
        public IActionResult PageNotFound()
        {

            return View();
        }
        [Route("/Found/404")]
        public IActionResult PageNotFoundAM()
        {

            return View();
        }


        public IActionResult AdminIndex()
		{
			return View();
		}

        public IActionResult Contact()
        {
            return View();
        }
    }
}
