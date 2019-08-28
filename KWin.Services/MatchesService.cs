using System;
using System.Collections.Generic;
using System.Text;
using KWin.Models;
using KWin.Data;
using System.Linq;

namespace KWin.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly BettingDbContext context;

        public MatchesService(BettingDbContext context)
        {
            this.context = context;
        }

        public ICollection<Match> GetAllMatches()
        {
            IList<Match> matches = this.context.Matches.ToList();

            return matches;
        }

        public Match GetMatchById(string id)
        {
            var match = this.context.Matches.Where(m => m.Id == id).FirstOrDefault();

            return match;
        }
    }
}
