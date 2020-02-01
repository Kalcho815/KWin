using KWin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWin.Services
{
    public interface IPlayersService
    {
        Task<ICollection<Player>> CreateAndGetPlayers(string TeamId);
    }
}