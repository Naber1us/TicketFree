using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Users;

namespace TicketFree.Features.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
