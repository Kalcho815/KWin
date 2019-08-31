using KWin.Data;
using KWin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Seeding
{
    public class KWinMatchSeeder : ISeeder
    {
        private const double minOddsRange = 1.20;
        private const double maxOddsRange = 2.10;
        private readonly BettingDbContext context;

        public KWinMatchSeeder(BettingDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            var matchesNotFinished = context.Matches.Where(m => m.Finished == false).Count() < 5;

            if (context.Matches.Where(m=>m.Finished == false).Count() < 5)
            {
                Team[] teams = context.Teams.ToArray();
                Random randomOdds = new Random();

                List<Match> matchesToAdd = new List<Match>();
                for (int i = 0; i < 5 - context.Matches.Where(m=>m.Finished == false).Count(); i++)
                {

                    Match match = new Match()
                    {
                        Finished = false,
                        //Adding 3 hours because DateTime.UtcNow is inaccurate
                        StartingTime = DateTime.UtcNow.AddHours(4),
                        League = teams[i].League,
                        FirstTeamToWinOdds = randomOdds.NextDouble() * (maxOddsRange - minOddsRange) + minOddsRange,
                        SecondTeamToWinOdds = randomOdds.NextDouble() * (maxOddsRange - minOddsRange) + minOddsRange,
                        DrawOdds = randomOdds.NextDouble() * (maxOddsRange - minOddsRange) + minOddsRange
                    };
                
                    matchesToAdd.Add(match);
                }

                context.Matches.AddRange(matchesToAdd);
                context.SaveChanges();

                MappingTeamsToMatches();
            }
        }

        private void MappingTeamsToMatches()
        {
            Random randomMatch = new Random();

            foreach (var team in context.Teams.ToList())
            {
                List<MatchTeam> matchTeams = new List<MatchTeam>();
                while (true)
                {
                    string randomMatchId = context.Matches.Where(m=> m.Finished == false).ToArray()[randomMatch.Next(0, 5)].Id;
                    if (context.Matches.Where(m => m.Id == randomMatchId).FirstOrDefault().MatchTeams.Count() < 2)
                    {
                        matchTeams.Add(new MatchTeam()
                        {
                            TeamId = team.Id,
                            MatchId = randomMatchId
                        });
                        team.MatchTeams = matchTeams;
                        context.SaveChanges();
                    }
                    else
                    {
                        continue;
                    }
                    break;
                }
            }
        }

    }
}
