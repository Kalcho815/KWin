using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class BettingUser : IdentityUser
    {
        public BettingUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Bets = new HashSet<Bet>();
        }

        [Required]
        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
