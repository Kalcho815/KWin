using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<ICollection<Team>> GetTeamsByMatchIdAsync(string matchId)
        {
            List<Team> teamsToReturn = new List<Team>();

            teamsToReturn = this.context
                .Teams.Where(t => t.MatchTeams.Any(mt => mt.MatchId == matchId)).ToList();

            return teamsToReturn;
        }

        public async Task<ICollection<Team>> GetAllTeamsAsync()
        {
            var teams = context.Teams.ToList();
            return teams;
        }
    }
}
