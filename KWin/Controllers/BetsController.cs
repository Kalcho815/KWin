using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Controllers
{
    public class BetsController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly ITeamsService teamsService;

        public BetsController(IMatchesService matchesService, ITeamsService teamsService)
        {
            this.matchesService = matchesService;
            this.teamsService = teamsService;
        }

        [HttpGet]
        public IActionResult MakeABet(string matchId)
        {
            var match = this.matchesService.GetMatchById(matchId);
            var teams = teamsService.GetTeamsByMatchId(match.Id).ToArray();

            MatchViewModel matchView = new MatchViewModel
            {
                FirstTeamName = teams[0].Name,
                SecondTeamName = teams[1].Name,
                StartingTime = match.StartingTime,
                FirstTeamOdds = match.FirstTeamToWinOdds.ToString("f2"),
                DrawOdds = match.DrawOdds.ToString("f2"),
                SecondTeamOdds = match.SecondTeamToWinOdds.ToString("f2"),
            };

            return this.View(matchView);
        }

        [HttpPost]
        [ActionName("MakeABet")]
        public IActionResult MakeABetPost(string typeOfBet, string amountOfBet, string userId)
        {

            throw new NotImplementedException();
        }
    }
}
