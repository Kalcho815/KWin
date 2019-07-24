using JetBrains.Annotations;
using KWin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWin.Data
{
    public class BettingDbContext : IdentityDbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Match { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<MatchTeam> MatchTeams { get; set; }

        public DbSet<BettingUser> Users { get; set; }

        public BettingDbContext(DbContextOptions<BettingDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MatchTeam>().HasKey(mt => new { mt.MatchId, mt.TeamId });

            builder.Entity<MatchTeam>()
            .HasOne<Team>(t => t.Team)
            .WithMany(mt => mt.MatchTeams)
            .HasForeignKey(x => x.TeamId);

            builder.Entity<MatchTeam>()
            .HasOne<Match>(t => t.Match)
            .WithMany(mt => mt.MatchTeams)
            .HasForeignKey(x => x.MatchId);
        }
    }
}
