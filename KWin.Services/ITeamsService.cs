using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWin.Services
{
    public interface ITeamsService
    {
        Task<ICollection<Team>> GetTeamsByMatchIdAsync(string matchId);

        Task<ICollection<Team>> GetAllTeamsAsync();
    }
}
