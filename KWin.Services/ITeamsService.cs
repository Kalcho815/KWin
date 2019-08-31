using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public interface ITeamsService
    {
        ICollection<Team> GetTeamsByMatchId(string matchId);

        ICollection<Team> GetAllTeams();
    }
}
