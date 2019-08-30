using KWin.Models.Enums;
using System;

namespace KWin.Models
{
    public class Bet
    {

        public Bet()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public DateTime MadeOn { get; set; }

        public decimal MoneyBet { get; set; }

        public double Odds { get; set; }

        public bool Won { get; set; }

        public string MatchId { get; set; }
        public Match Match { get; set; }

        public BetType BetType { get; set; }

        public string BettorId { get; set; }
        public BettingUser Bettor { get; set; }
    }
}
