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
            // 1 park heeft meerdere huizen
            modelBuilder.Entity<Park>()
                .HasMany(p => p.Huizen)
                .WithOne(h => h.Park);

            // Een huurcontract is gebonden aan een huis en een huurder
            modelBuilder.Entity<Huurcontract>()
                .HasOne(hc => hc.Huis)
                .WithMany(h => h.Huurcontracten)
                .HasForeignKey(hc => hc.HuisId);

            modelBuilder.Entity<Huurcontract>()
                .HasOne(hc => hc.Huurder)
                .WithMany()
                .HasForeignKey(hc => hc.HuurderId); // foreign key
        
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