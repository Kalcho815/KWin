using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWin.Data;
using KWin.Models;

namespace KWin.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly BettingDbContext context;

        public TeamsService(BettingDbContext context)
        {
            this.context = context;
        }

        public ICollection<Team> GetTeamsByMatchId(string matchId)
        {
            List<Team> teamsToReturn = new List<Team>();

            //teamsToReturn = this.context.Matches
            //    .Where(m => m.Id == matchId)
            //    .FirstOrDefault()
            //    .MatchTeams
            //    .Select(mt => mt.Team)
            //    .ToList();

            teamsToReturn = this.context
                .Teams.Where(t => t.MatchTeams.Any(mt => mt.MatchId == matchId)).ToList();

            return teamsToReturn;
        }
    }
}
