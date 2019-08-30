using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class Match
    {
        public Match()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Bets = new HashSet<Bet>();
            this.MatchTeams = new List<MatchTeam>();
        }
        public string Id { get; set; }

        public ICollection<MatchTeam> MatchTeams { get; set; }

        [Required]
        public double FirstTeamToWinOdds { get; set; }

        [Required]
        public double DrawOdds { get; set; }

        [Required]
        public double SecondTeamToWinOdds { get; set; }

        [Required]
        [MaxLength(30)]
        public string League { get; set; }

        [Required]
        public DateTime StartingTime { get; set; }

        public ICollection<Bet> Bets { get; set; }

        public bool Finished { get; set; }

        public string Result { get; set; }
    }
}
