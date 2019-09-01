using KWin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Services
{
    public interface IUsersService
    {
        void ReduceBalance(decimal amount, string userId);

        ICollection<BettingUser> GetAllUsers();

        void IncreaseUserBalanceByUsername(string username, decimal amount);
    }
}
