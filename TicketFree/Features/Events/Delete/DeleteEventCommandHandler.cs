using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events.Dto;
using TicketFree.Interfaces;

namespace TicketFree.Features.Events.Delete
{
    public class CloseEventByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<CloseEventByIdQuery, int?>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<int?> Handle(CloseEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.EventsInfo
                            .Where(e => e.EventId == request.Id)
                            .ExecuteUpdateAsync(s => s
                                .SetProperty(b => b.EventStatus, b => "Close"), cancellationToken: cancellationToken);


        }
    }
}
