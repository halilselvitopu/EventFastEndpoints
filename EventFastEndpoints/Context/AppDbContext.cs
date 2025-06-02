using EventFastEndpoints.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.OwnsOne(e => e.Amenities, a =>
                {
                    a.ToJson(); // Amenities -> jsonb
                });

                entity.OwnsOne(e => e.LocationInfo, loc =>
                {
                    loc.ToJson(); // LocationInfo -> jsonb
                });
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.OwnsOne(e => e.SponsorDetails, a =>
                {
                    a.ToJson(); // SponsorDetails -> jsonb
                });

            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.OwnsOne(e => e.ContactInfo, a =>
                {
                    a.ToJson(); // ContactInfo -> jsonb
                });

            });

        }
    }
}
