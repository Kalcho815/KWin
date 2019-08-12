using System;
using System.Collections.Generic;

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

        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }

        public int PositionInLeague { get; set; }

        public ICollection<MatchTeam> MatchTeams { get; set; }

        public string League { get; set; }
    }
}
