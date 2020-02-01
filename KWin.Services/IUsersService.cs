using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWin.Services
{
    public interface IUsersService
    {
        Task ReduceBalanceAsync(decimal amount, string userId);

        Task<ICollection<BettingUser>> GetAllUsersAsync();

        Task IncreaseUserBalanceByUsernameAsync(string username, decimal amount);
    }
}
