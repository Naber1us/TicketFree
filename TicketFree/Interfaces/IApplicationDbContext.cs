using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events;
using TicketFree.Features.Places;
using TicketFree.Features.Tickets;
using TicketFree.Features.Users;

namespace TicketFree.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> UsersInfo { get; }
        DbSet<Ticket> TicketsInfo { get; }
        DbSet<Place> PlacesInfo { get; }
        DbSet<Event> EventsInfo { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
