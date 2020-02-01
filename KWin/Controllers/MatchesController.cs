using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly ITeamsService teamsService;

        public MatchesController(IMatchesService matchesService, ITeamsService teamsService)
        {
            this.matchesService = matchesService;
            this.teamsService = teamsService;
        }

        [Authorize]
        public async Task<IActionResult> AllMatches()
        {
            var allMatches = await this.matchesService.GetAllMatchesAsync();
            var matchesForView = new List<MatchViewModel>();

            foreach (var match in allMatches)
            {
                var matchForView = new MatchViewModel
                {
                    MatchId = match.Id,
                    FirstTeamName = match.MatchTeams.ToArray()[0].Team.Name,
                    SecondTeamName = match.MatchTeams.ToArray()[1].Team.Name,
                    StartingTime = match.StartingTime,
                    FirstTeamOdds = match.FirstTeamToWinOdds.ToString("f2"),
                    DrawOdds = match.DrawOdds.ToString("f2"),
                    SecondTeamOdds = match.SecondTeamToWinOdds.ToString("f2"),
                    Result = match.Result
                };
                matchesForView.Add(matchForView);
            }

            var matchesViewModel = new MatchesViewModel()
            {
                matches = matchesForView.OrderByDescending(m=>m.StartingTime).ToList()
            };

            return this.View(matchesViewModel);
        }
    }
}
