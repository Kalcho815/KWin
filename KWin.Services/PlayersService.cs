using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public class PlayersService : IPlayersService
    {

        public ICollection<Player> CreateAndGetPlayers(string teamId)
        {
            string[] playerFirstNames = new string[]
            {
                "Joshua",
                "Daniel",
                "David",
                "Joseph",
                "Jason",
                "John",
                "Andrew",
                "Jesse",
                "Benjamin",
                "Mark",
                "Aaron",
                "Stephen",
            };

            string[] playerLastNames = new string[]
            {
                "Wright",
                "Cox",
                "Gibson",
                "Simpson",
                "Reynolds",
                "Palmer",
                "Baker",
                "Harris",
                "Ward",
                "Gray",
                "Parker",
                "Gordon",
            };

            Random random = new Random();

            List<Player> players = new List<Player>();

            for (int i = 0; i < 11; i++)
            {
                string randomFirstName = playerFirstNames[random.Next(0, 11)];

                string randomLastName = playerLastNames[random.Next(0, 11)];

                Player player = new Player
                {
                    FullName = randomFirstName + " " + randomLastName,
                    TeamId = teamId,
                };

                players.Add(player);
            }

            return players;
        }
    }
}
