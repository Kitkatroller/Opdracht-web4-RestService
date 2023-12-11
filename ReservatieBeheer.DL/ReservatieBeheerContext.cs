using Microsoft.EntityFrameworkCore;
using ReservatieBeheer.BL.Models;
using ReservatieBeheer.DL.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservatieBeheer.DL
{
    public class ReservatieBeheerContext : DbContext
    {
        public ReservatieBeheerContext(DbContextOptions<ReservatieBeheerContext> options)
            : base(options)
        {
        }

        public DbSet<TafelEF> Tafels { get; set; }
        public DbSet<ReservatieEF> Reservaties { get; set; }
        public DbSet<KlantEF> Klanten { get; set; }
        public DbSet<RestaurantEF> Restaurants { get; set; }
        public DbSet<LocatieEF> Locaties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservatieEF>()
                .HasOne(res => res.Tafel)
                .WithMany()
                .HasForeignKey(res => res.TafelNummer);

            modelBuilder.Entity<ReservatieEF>()
                .HasOne(res => res.ContactPersoon)
                .WithMany(klant => klant.Reservaties)
                .HasForeignKey(res => res.KlantID);

            modelBuilder.Entity<ReservatieEF>()
                .HasOne(res => res.Restaurant)
                .WithMany(restaurant => restaurant.Reservaties)
                .HasForeignKey(res => res.RestaurantID);

            modelBuilder.Entity<KlantEF>()
                .HasOne(k => k.Locatie)
                .WithMany()
                .HasForeignKey(k => k.LocatieID);

            modelBuilder.Entity<RestaurantEF>()
                .HasOne(r => r.Locatie)
                .WithMany()
                .HasForeignKey(r => r.LocatieID);
        }
    }
}
