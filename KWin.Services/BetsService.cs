using KWin.Data;
using KWin.Models;
using KWin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWin.Services
{
    public class BetsService : IBetsService
    {
        private readonly BettingDbContext context;

        public BetsService(BettingDbContext context)
        {
            this.context = context;
        }
        public void CreateBet(string matchId, string bettorId, decimal moneyBet, string betType)
        {
            double odds = 0;

            switch (betType)
            {
                case "1":
                    odds = context.Matches.Where(m => m.Id == matchId).FirstOrDefault().FirstTeamToWinOdds;
                    break;
                case "X":
                    odds = context.Matches.Where(m => m.Id == matchId).FirstOrDefault().DrawOdds;
                    break;
                case "2":
                    odds = context.Matches.Where(m => m.Id == matchId).FirstOrDefault().SecondTeamToWinOdds;
                    break;
                default:
                    break;
            }

            Bet betToCreate = new Bet
            {
                BettorId = bettorId,
                BetType = (BetType)Enum.Parse(typeof(BetType), betType),
                MadeOn = DateTime.UtcNow,
                MatchId = matchId,
                MoneyBet = moneyBet,
                Odds = odds
            };

            
            context.Bets.Add(betToCreate);
            this.context.SaveChanges();
        }
    }
}
