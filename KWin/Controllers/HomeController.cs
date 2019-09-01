using KWin.Models;
using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            matchesService.DeleteOldMatches();
            matchesService.CheckAndGiveResultsToMatches();

            var matches = matchesService.GetUnfinishedMatches();
            var matchViewModels = new List<MatchViewModel>();

            foreach (var match in matches)
            {
                var matchViewModel = new MatchViewModel
                {
                    FirstTeamName = match.MatchTeams.ToArray()[0].Team.Name,
                    SecondTeamName = match.MatchTeams.ToArray()[1].Team.Name,
                    MatchId = match.Id
                };

                matchViewModels.Add(matchViewModel);
            }


            return View(matchViewModels);
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
