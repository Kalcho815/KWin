using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Models.Bets
{
    public class BetViewModel
    {
        public DateTime MatchDate { get; set; }

        public string FirstTeam { get; set; }

        public string SecondTeam { get; set; }

        public string BetType { get; set; }

        public string MoneyBet { get; set; }

        public DateTime BetDate { get; set; }

        public string Odds { get; set; }

        public string Won { get; set; }
    }
}
