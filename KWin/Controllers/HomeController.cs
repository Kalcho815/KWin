using KWin.Models;
using KWin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KWin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchesService matchesService;

        public HomeController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public IActionResult Index()
        {
            matchesService.CheckAndGiveResultsToMatches();
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
