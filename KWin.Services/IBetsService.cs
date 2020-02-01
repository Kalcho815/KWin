using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWin.Services
{
    public interface IBetsService
    {
        Task CreateBetAsync(string matchId, string bettorId, decimal moneyBet, string betType);

        Task<ICollection<Bet>> GetBetsByUserIdAsync(string userId);

        Task CheckAndPayoutBetsAsync(string userId);
    }
}
