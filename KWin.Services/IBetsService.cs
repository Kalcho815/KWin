using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public interface IBetsService
    {
        void CreateBet(string matchId, string bettorId, decimal moneyBet, string betType);

        ICollection<Bet> GetBetsByUserId(string userId);

        void CheckAndPayoutBets(string userId);
    }
}
