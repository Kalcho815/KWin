﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Models
{
    public class Match
    {
        public Match()
        {
            this.Id = new Guid().ToString();
            this.Bets = new HashSet<Bet>();
            this.MatchTeams = new List<MatchTeam>();
        }
        public string Id { get; set; }

        public ICollection<MatchTeam> MatchTeams {get;set;}

        public string League { get; set; }

        public DateTime StartingTime { get; set; }

        public ICollection<Bet> Bets { get; set; }

        public bool Finished { get; set; }

        public string Result { get; set; }
    }
}
