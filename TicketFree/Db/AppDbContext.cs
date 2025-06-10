using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TicketFree.Features.Users;
using TicketFree.Features.Tickets;
using TicketFree.Features.Places;
using TicketFree.Features.Events;
using TicketFree.Interfaces;

namespace TicketFree.Db
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options), IApplicationDbContext
    {
        public DbSet<User> UsersInfo { get; set; } = null!;
        public DbSet<Ticket> TicketsInfo { get; set; } = null!;
        public DbSet<Place> PlacesInfo { get; set; } = null!;
        public DbSet<Event> EventsInfo { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
