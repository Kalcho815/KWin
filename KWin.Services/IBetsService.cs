using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public interface IBetsService
    {
        void CreateBet(string matchId, string bettorId, decimal moneyBet, string betType);
    }
}
