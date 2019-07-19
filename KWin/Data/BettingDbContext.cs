using KWin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Data
{
    public class BettingDbContext : IdentityDbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<Match> Match { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public BettingDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
    }
}
