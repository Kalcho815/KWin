using KWin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Models
{
    public class Bet
    {

        public Bet()
        {
            this.Id = new Guid().ToString();
        }
        public string Id { get; set; }

        public DateTime MadeOn { get; set; }

        public decimal MoneyBet { get; set; }

        public double Odds { get; set; }

        public Match Match { get; set; }

        public BetType BetType { get; set; }

        public User Bettor { get; set; }
    }
}
