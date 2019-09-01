using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Models.Matches
{
    public class MatchViewModel
    {
        public string MatchId { get; set; }

        public string FirstTeamName { get; set; }

        public string SecondTeamName { get; set; }

        public DateTime StartingTime { get; set; }

        public string FirstTeamOdds { get; set; }

        public string SecondTeamOdds { get; set; }

        public string DrawOdds { get; set; }

        public string Result { get; set; }

        public string Error { get; set; }

    }
}
