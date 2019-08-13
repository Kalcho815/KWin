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
        private List<MatchTeam> participatingTeams;

        public KWinMatchSeeder(BettingDbContext context)
        {
            this.context = context;
            this.participatingTeams = new List<MatchTeam>();
        }

        public void Seed()
        {
            if (context.Matches.Count() < 5)
            {


                Team[] teams = context.Teams.ToArray();
                Random random = new Random();
                List<int> takenTeams = new List<int>();

                for (int i = 0; i < 5 - context.Matches.ToArray().Length; i++)
                {
                    List<Team> twoRandomTeams = GetTwoRandomTeams( random, teams);


                    participatingTeams.Add(
                       new MatchTeam
                       {
                           TeamId = twoRandomTeams[0].Id,
                           Team = twoRandomTeams[0],

                       }
                   );

                    participatingTeams.Add(
                        new MatchTeam
                        {
                            TeamId = twoRandomTeams[1].Id,
                            Team = twoRandomTeams[1]

                        }
                    );

                    Match match = new Match()
                    {
                        Finished = false,
                        StartingTime = DateTime.UtcNow.AddHours(1),
                        League = teams[i].League,
                        MatchTeams = participatingTeams
                    };

                    

                    context.Matches.Add(match);
                    context.SaveChanges();

                    twoRandomTeams.Clear();
                    participatingTeams.Clear();
                }
            }
        }

        private List<Team> GetTwoRandomTeams(Random random, Team[] teams)
        {

            int firstTeam = 0;
            List<Team> teamsToReturn = new List<Team>();
            while (true)
            {
                firstTeam = random.Next(0, 9);
                Team firstTeamToReturn = teams[firstTeam];
                if (context.MatchTeams.Select(mt=>mt.TeamId).Contains(firstTeamToReturn.Id))
                {
                    continue;
                }
                teamsToReturn.Add(firstTeamToReturn);
                break;
            }

            while (true)
            {
                int secondTeam = random.Next(0, 9);
                Team secondTeamToReturn = teams[secondTeam];
                if (context.MatchTeams.Select(mt => mt.TeamId).Contains(secondTeamToReturn.Id)
                    || secondTeamToReturn.Id == teams[firstTeam].Id)
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
