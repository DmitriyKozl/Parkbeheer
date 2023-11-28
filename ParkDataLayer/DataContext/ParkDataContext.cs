using System;
using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;

namespace ParkDataLayer.DataContext {
    public class ParkDataContext : DbContext {
        public DbSet<Huis> Huizen { get; set; }

        public DbSet<Huurcontract> Huurcontracten { get; set; }

        public DbSet<Huurder> Huurders { get; set; }

        public DbSet<Park> Parken { get; set; }

        private readonly string _connectionstring;

        public ParkDataContext(string connectionstring) {
            this._connectionstring = connectionstring;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //
          // // 1 park heeft meerdere huizen  
          //   modelBuilder.Entity<Park>()
          //       .HasMany(p => p.Huizen)
          //       .WithOne(h => h.Park);
          //
          //   // Huis kan meerder huurcontracten hebben
          //   modelBuilder.Entity<Huis>()
          //       .HasMany(h => h.Huurcontracten)
          //       .WithOne(hc => hc.Huis);
          //
          //   // Een huurcontract is gebonden aan een huurder
          //   modelBuilder.Entity<Huurcontract>()
          //       .HasOne(hc => hc.Huurder)
          //       .WithMany()
          //       .HasForeignKey(hc => hc.HuurderId); // foreign key
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_connectionstring)
                .LogTo(
                    Console.WriteLine,
                    Microsoft
                        .Extensions
                        .Logging
                        .LogLevel
                        .Information
                    );
        }
    }
}