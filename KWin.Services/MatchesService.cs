using System;
using System.Collections.Generic;
using System.Text;
using KWin.Models;
using KWin.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KWin.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly BettingDbContext context;

        public MatchesService(BettingDbContext context)
        {
            this.context = context;
        }

        public async Task CheckAndGiveResultsToMatchesAsync()
        {
            var matches = context.Matches.ToList();

            foreach (var match in matches)
            {
                if (match.StartingTime.AddMinutes(90) < DateTime.UtcNow)
                {
                    if (!match.Finished)
                    {
                        match.Result = await GiveRandomResultAsync();
                        match.Finished = true;
                    }
                }
                else
                {
                    match.Result = "Not finished";
                }
            }
            context.SaveChanges();
        }

        private async Task<string> GiveRandomResultAsync()
        {
            Random percentages = new Random();
            Random goals = new Random();

            string resultToPass = string.Empty;

            string[] result = new string[2];

            int firstTeamGoals = 0;
            int secondTeamGoals = 0;
            if (percentages.Next(0, 10) < 7)
            {
                firstTeamGoals = goals.Next(0, 3);
            }
            else
            {
                firstTeamGoals = goals.Next(3, 5);
            }

            if (percentages.Next(0, 10) < 7)
            {
                secondTeamGoals = goals.Next(0, 3);
            }
            else
            {
                secondTeamGoals = goals.Next(3, 5);
            }

            result[0] = firstTeamGoals.ToString();
            result[1] = secondTeamGoals.ToString();

            resultToPass = result[0] + " - " + result[1];

            return resultToPass;
        }

        public async Task<ICollection<Match>> GetAllMatchesAsync()
        {
            IList<Match> matches = this.context.Matches.Include(m=>m.MatchTeams).ThenInclude(mt=>mt.Team).ToList();

            return matches;
        }

        public async Task<Match> GetMatchByIdAsync(string id)
        {
            var match = this.context.Matches.Where(m => m.Id == id).FirstOrDefault();

            return match;
        }

        public async Task DeleteOldMatchesAsync()
        {
            var matchesToDelete = context.Matches
                .Where(m => m.Finished == true && m.StartingTime.AddDays(1) < DateTime.UtcNow);
            context.Matches.RemoveRange(matchesToDelete);
        }

        public async Task<ICollection<Match>> GetUnfinishedMatchesAsync()
        {
            IList<Match> matches = this.context.Matches
                .Where(m=>m.Finished==false)
                .Include(m => m.MatchTeams)
                .ThenInclude(mt => mt.Team).ToList();

            return matches;
        }
    }
}
