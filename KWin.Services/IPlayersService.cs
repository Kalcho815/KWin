using KWin.Models;
using System.Collections.Generic;

namespace KWin.Services
{
    public interface IPlayersService
    {
        ICollection<Player> CreateAndGetPlayers(string TeamId);
    }
}