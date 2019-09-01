using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public interface IMatchesService
    {
        ICollection<Match> GetAllMatches();

        ICollection<Match> GetUnfinishedMatches();

        Match GetMatchById(string id);

        void CheckAndGiveResultsToMatches();

        void DeleteOldMatches();
    }
}
