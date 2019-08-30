using KWin.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class Bet
    {

        public Bet()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        public DateTime MadeOn { get; set; }

        [Required]
        [Range(0.5, double.MaxValue)]
        public decimal MoneyBet { get; set; }

        [Required]
        public double Odds { get; set; }

        public bool Won { get; set; }

        [Required]
        public string MatchId { get; set; }
        public Match Match { get; set; }

        [Required]
        public BetType BetType { get; set; }

        [Required]
        public string BettorId { get; set; }
        public BettingUser Bettor { get; set; }
    }
}
