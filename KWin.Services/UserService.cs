﻿using System;
using System.Collections.Generic;
using System.Text;
using KWin.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Web;
using KWin.Data;

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

        public void ReduceBalance(decimal amount, string userId)
        {
            context.Users.FirstOrDefault(u => u.Id == userId).Balance -= amount;
            context.SaveChanges();
        }
    }
}