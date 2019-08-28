using KWin.Models;
using KWin.Models.Bets;
using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<BettingUser> _userManager;
        private readonly IBetsService betsService;
        private readonly IUsersService usersService;

        public BetsController(IMatchesService matchesService, ITeamsService teamsService
            , UserManager<BettingUser> _userManager
            , IBetsService betsService
            , IUsersService usersService)
        {
            this.matchesService = matchesService;
            this.teamsService = teamsService;
            this._userManager = _userManager;
            this.betsService = betsService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult MakeABet(string matchId, string errorMessage)
        {
            var match = this.matchesService.GetMatchById(matchId);
            var teams = teamsService.GetTeamsByMatchId(match.Id).ToArray();

            if (errorMessage != "Not enough balance")
            {
                errorMessage = "";
            }

            MatchViewModel matchView = new MatchViewModel
            {
                FirstTeamName = teams[0].Name,
                SecondTeamName = teams[1].Name,
                StartingTime = match.StartingTime,
                FirstTeamOdds = match.FirstTeamToWinOdds.ToString("f2"),
                DrawOdds = match.DrawOdds.ToString("f2"),
                SecondTeamOdds = match.SecondTeamToWinOdds.ToString("f2"),
                MatchId = matchId,
                Error = errorMessage
            };

            return this.View(matchView);
        }

        [HttpPost]
        [ActionName("MakeABet")]
        public IActionResult MakeABetPost(BetCreateBindingModel model)
        {
            var userBalance = this._userManager.GetUserAsync(this.User).Result.Balance;
            if (model.MoneyBet > userBalance)
            {
                return this.MakeABet(model.MatchId, "Not enough balance");
            }

            this.betsService.CreateBet(model.MatchId, model.UserId, model.MoneyBet, model.BetType);
            this.usersService.ReduceBalance(model.MoneyBet, model.UserId);

            return this.Redirect("/Matches/AllMatches");
        }
    }
}
