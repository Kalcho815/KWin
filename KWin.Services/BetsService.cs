using KWin.Data;
using KWin.Models;
using KWin.Models.Enums;
using Microsoft.EntityFrameworkCore;
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
                Bettor = context.Users.Where(u => u.Id == bettorId).FirstOrDefault(),
                BetType = (BetType)Enum.Parse(typeof(BetType), betType),
                MadeOn = DateTime.UtcNow,
                MatchId = matchId,
                Match = context.Matches.Where(m => m.Id == matchId).FirstOrDefault(),
                MoneyBet = moneyBet,
                Odds = odds
            };

            
            context.Bets.Add(betToCreate);
            this.context.SaveChanges();
        }

        public ICollection<Bet> GetBetsByUserId(string userId)
        {
            var bets = context.Bets
                .Where(b => b.BettorId == userId)
                .Include(b => b.Bettor)
                .Include(b=>b.Match)
                .ThenInclude(m=>m.MatchTeams)
                .ThenInclude(mt=>mt.Team)
                .ToArray();
            return bets;
        }
    }
}
