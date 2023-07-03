using Domain.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<FootballPitch> FootballPitches { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<GameClass> GameClasses { get; set; }
        public DbSet<FootballPitchRequest> FootballPitchRequests { get; set; }
        public DbSet<MatchRequest> MatchRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BranchClub> BranchesClubs { get;set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Club>()
        //    //.HasOne(k => k.User)
        //    //.WithOne(u => u.Club)
        //    //.HasForeignKey<User>(u => u.Club);
        //}

    }
}
