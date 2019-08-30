using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new HashSet<Player>();
            this.MatchTeams = new List<MatchTeam>();
        }
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }

        public int PositionInLeague { get; set; }

        public ICollection<MatchTeam> MatchTeams { get; set; }

        [MaxLength(30)]
        public string League { get; set; }
    }
}
