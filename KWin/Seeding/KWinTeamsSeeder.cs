using KWin.Data;
using KWin.Models;
using KWin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWin.Seeding
{
    public class KWinTeamsSeeder : ISeeder
    {
        private readonly BettingDbContext dbContext;
        private readonly IPlayersService playersService;

        public KWinTeamsSeeder(BettingDbContext dbContext, IPlayersService playersService)
        {
            this.playersService = playersService;
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            string[] footballTeamNames = new string[]
            {
                "Mansfield Town",
                "Cambridge United",
                "Bradford City",
                "Colchester United",
                "Northampton Town",
                "Scunthorpe United",
                "Stevenage",
                "Walsall",
                "Crewe Alexandra",
                "Swindon Town"
            };

            if (dbContext.Teams.ToList().Count <= 10)
            {
                int teamCount = dbContext.Teams.ToList().Count;



                for (int i = 0; i < 10 - teamCount; i++)
                {
                    Team team = new Team
                    {
                        League = "PL",
                        Name = footballTeamNames[i],
                    };

                    team.Players = this.playersService.CreateAndGetPlayers(team.Id);
                    this.dbContext.Teams.Add(team);
                }
                this.dbContext.SaveChanges();
            }
        }
    }
}
