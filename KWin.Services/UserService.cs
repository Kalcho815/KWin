using System;
using System.Collections.Generic;
using System.Text;
using KWin.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Web;
using KWin.Data;
using System.Threading.Tasks;

namespace KWin.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<BettingUser> userManager;
        private readonly BettingDbContext context;

        public UsersService(UserManager<BettingUser> userManager, BettingDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<ICollection<BettingUser>> GetAllUsersAsync()
        {
            var usersToReturn = context.Users.ToList();

            return usersToReturn;
        }

        public async Task IncreaseUserBalanceByUsernameAsync(string username, decimal amount)
        {
            var user = this.context.Users.Where(u => u.UserName == username).FirstOrDefault();

            user.Balance += amount;
            context.SaveChanges();
        }

        public async Task ReduceBalanceAsync(decimal amount, string userId)
        {
            context.Users.FirstOrDefault(u => u.Id == userId).Balance -= amount;
            context.SaveChanges();
        }
    }
}
