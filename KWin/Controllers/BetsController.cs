using KWin.Models;
using KWin.Models.Bets;
using KWin.Models.Matches;
using KWin.Services;
using Microsoft.AspNetCore.Authorization;
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
            , IUsersService usersService
            )
        {
            this.matchesService = matchesService;
            this.teamsService = teamsService;
            this._userManager = _userManager;
            this.betsService = betsService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize]
        [ActionName("MakeABet")]
        public async Task<IActionResult> MakeABetAsync(string matchId, string errorMessage)
        {
            var match = await this.matchesService.GetMatchByIdAsync(matchId);
            if (match.Finished)
            {
                return this.Redirect("/Matches/AllMatches");
            }
            var teams = await teamsService.GetTeamsByMatchIdAsync(match.Id);
            
            if (errorMessage == null)
            {
                errorMessage = "";
            }

            MatchViewModel matchView = new MatchViewModel
            {
                FirstTeamName = teams.ToArray()[0].Name,
                SecondTeamName = teams.ToArray()[1].Name,
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
        [Authorize]
        public async Task<IActionResult> MakeABetPost(BetCreateBindingModel model)
        {
            var userBalance = this._userManager.GetUserAsync(this.User).Result.Balance;
            if (model.MoneyBet > userBalance)
            {
                return await this.MakeABetAsync(model.MatchId, "Not enough balance");
            }
            else if (model.MoneyBet<=1)
            {
                return await this.MakeABetAsync(model.MatchId, "Bet ammount can not be below 1");
            }

            if (ModelState.IsValid)
            {
                await this.betsService.CreateBetAsync(model.MatchId, model.UserId, model.MoneyBet, model.BetType);
                await this.usersService.ReduceBalanceAsync(model.MoneyBet, model.UserId);
            }
            else
            {
                return await this.MakeABetAsync(model.MatchId, ModelState.ValidationState.ToString());
            }

            return this.Redirect("/Matches/AllMatches");
        }

        [Authorize]
        public async Task<IActionResult> MyBets()
        {
            await this.betsService.CheckAndPayoutBetsAsync(this._userManager.GetUserAsync(this.User).Result.Id);
            var userBets = await this.betsService.GetBetsByUserIdAsync(this._userManager.GetUserAsync(this.User).Result.Id);

            var betViewModels = new List<BetViewModel>();


            foreach (var bet in userBets)
            {
                string betWinToDisplay = string.Empty;
                if (bet.Won)
                {
                    betWinToDisplay = "Yes";
                }
                else if (bet.Match.Finished == false)
                {
                    betWinToDisplay = "Match is not finished";
                }
                else 
                {
                    betWinToDisplay = "No";
                }

                var typeOfBet = "";
                if (bet.BetType.ToString() == "One")
                {
                    typeOfBet = "1";
                }
                else if (bet.BetType.ToString() == "X")
                {
                    typeOfBet = "X";
                }
                else if (bet.BetType.ToString() == "2")
                {
                    typeOfBet = "2";
                }
                
                var betViewModel = new BetViewModel()
                {
                    BetDate = bet.MadeOn,
                    FirstTeam = bet.Match.MatchTeams.ToArray()[0].Team.Name,
                    SecondTeam = bet.Match.MatchTeams.ToArray()[1].Team.Name,
                    MatchDate = bet.Match.StartingTime,
                    BetType = typeOfBet,
                    MoneyBet = bet.MoneyBet.ToString(),
                    Odds = bet.Odds.ToString("f2"),
                    Won = betWinToDisplay
                };
                betViewModels.Add(betViewModel);
            }

            return this.View(betViewModels.ToArray());
        }
    }
}
