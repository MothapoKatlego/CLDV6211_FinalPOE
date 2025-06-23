using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace KatEventPlan.Models
{
    public class EventDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Booking> Bookings { get; set; }

     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Event
            modelBuilder.Entity<Event>()
                .Property(e => e.Event_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .IsUnicode(false);

            
            // Configure Venue
            modelBuilder.Entity<Venue>()
                .Property(e => e.Venue_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Venue>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Venue>()
                .Property(e => e.Image_Url)
                .IsUnicode(false);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Venue)
                .WillCascadeOnDelete(false);


            // Call base ONCE
            base.OnModelCreating(modelBuilder);
        }
    }
}
