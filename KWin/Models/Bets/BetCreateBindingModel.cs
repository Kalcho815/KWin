using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KWin.Models.Bets
{
    public class BetCreateBindingModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The amount of the bet cannot be below 1")]
        public decimal MoneyBet { get; set; }

        public string BetType { get; set; }

        public string MatchId { get; set; }

        public string UserId { get; set; }

    }
}
