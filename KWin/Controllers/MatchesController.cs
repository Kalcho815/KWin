using KWin.Models.Matches;
using KWin.Services;
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

        public IActionResult AllMatches()
        {
            var allMatches = this.matchesService.GetAllMatches();
            var matchesForView = new List<MatchViewModel>();

            foreach (var match in allMatches)
            {
                var teams = teamsService.GetTeamsByMatchId(match.Id).ToArray();
                var matchForView = new MatchViewModel
                {
                    FirstTeamName = teams[0].Name,
                    SecondTeamName = teams[1].Name,
                    StartingTime = match.StartingTime
                };
                matchesForView.Add(matchForView);
            }

            var matchesViewModel = new MatchesViewModel()
            {
                matches = matchesForView
            };

            return this.View(matchesViewModel);
        }
    }
}
