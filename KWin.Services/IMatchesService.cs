using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWin.Services
{
    public interface IMatchesService
    {
        Task<ICollection<Match>> GetAllMatchesAsync();

        Task<ICollection<Match>> GetUnfinishedMatchesAsync();

        Task<Match> GetMatchByIdAsync(string id);

        Task CheckAndGiveResultsToMatchesAsync();

        Task DeleteOldMatchesAsync();
    }
}
