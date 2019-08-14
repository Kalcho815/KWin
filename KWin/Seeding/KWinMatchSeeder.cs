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
        private readonly BettingDbContext context;

        public KWinMatchSeeder(BettingDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (context.Matches.Count() < 5)
            {


                Team[] teams = context.Teams.ToArray();
                Random random = new Random();

                List<MatchTeam> participatingTeams = new List<MatchTeam>();
                List<Match> matchesToAdd = new List<Match>();
                //TODO: Fix
                for (int i = 0; i < 5 - context.Matches.Count(); i++)
                {

                    Match match = new Match()
                    {
                        Finished = false,
                        StartingTime = DateTime.UtcNow.AddHours(1),
                        League = teams[i].League,
                    };

                    //match.MatchTeams = participatingTeams;

                    matchesToAdd.Add(match);
                }

                context.Matches.AddRange(matchesToAdd);
                context.SaveChanges();

                //context.Teams.ToList().Select(t => t.MatchTeams = new MatchTeam
                //{
                //    TeamId = t.Id,
                //    MatchId = matchesToAdd.ToArray()[randomMatch.Next(0, 5)].Id
                //});
            }
            MappingTeamsToMatches();

        }

        private void MappingTeamsToMatches()
        {
            Random randomMatch = new Random();

            foreach (var team in context.Teams.ToList())
            {
                List<MatchTeam> matchTeams = new List<MatchTeam>();
                while (true)
                {
                    string randomMatchId = context.Matches.ToArray()[randomMatch.Next(0, 5)].Id;
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

        private List<Team> GetTwoRandomTeams(Random random, Team[] teams, List<MatchTeam> participatingTeams)
        {

            int firstTeam = 0;
            List<Team> teamsToReturn = new List<Team>();
            while (true)
            {
                firstTeam = random.Next(0, 10);
                Team firstTeamToReturn = teams[firstTeam];
                if (context.MatchTeams.Select(mt => mt.TeamId).Contains(firstTeamToReturn.Id)
                    || participatingTeams.Any(t => t.TeamId == firstTeamToReturn.Id))
                {
                    continue;
                }
                teamsToReturn.Add(firstTeamToReturn);
                break;
            }

            while (true)
            {
                int secondTeam = random.Next(0, 10);
                Team secondTeamToReturn = teams[secondTeam];


                if (context.MatchTeams.Select(mt => mt.TeamId).Contains(secondTeamToReturn.Id)
                    || secondTeamToReturn.Id == teams[firstTeam].Id
                    || participatingTeams.Any(t => t.TeamId == secondTeamToReturn.Id))
                {
                    continue;
                }
                teamsToReturn.Add(secondTeamToReturn);
                break;
            }

            return teamsToReturn;
        }
    }
}
