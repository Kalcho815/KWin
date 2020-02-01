using KWin.Models;
using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchesService matchesService;

        public HomeController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public async Task<IActionResult> Index()
        {
            await matchesService.DeleteOldMatchesAsync();
            await matchesService.CheckAndGiveResultsToMatchesAsync();
            
            var matches = await matchesService.GetUnfinishedMatchesAsync();
            var matchViewModels = new List<MatchViewModel>();

            foreach (var match in matches)
            {
                var matchViewModel = new MatchViewModel
                {
                    FirstTeamName = match.MatchTeams.ToArray()[0].Team.Name,
                    SecondTeamName = match.MatchTeams.ToArray()[1].Team.Name,
                    MatchId = match.Id,
                    StartingTime = match.StartingTime
                };

                matchViewModels.Add(matchViewModel);
            }
            


            return View(matchViewModels);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
