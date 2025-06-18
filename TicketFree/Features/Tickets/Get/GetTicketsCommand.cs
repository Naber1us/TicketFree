using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Tickets.Dto;
using TicketFree.Interfaces;

namespace TicketFree.Features.Tickets.Get
{
    public class GetTicketByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTicketByIdQuery, TicketDto?>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.TicketsInfo
                .Where(t => t.TicketId == request.Id)
                .Select(t => new TicketDto
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId,
                    UserId = t.UserId
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetTicketsByUserQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTicketsByUserQuery, List<TicketDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<TicketDto>> Handle(GetTicketsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.TicketsInfo
                .Where(t => t.UserId == request.Id)
                .Select(t => new TicketDto
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId,
                    UserId = t.UserId
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }

    public class GetTicketsByEventQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTicketsByEventQuery, List<TicketDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<TicketDto>> Handle(GetTicketsByEventQuery request, CancellationToken cancellationToken)
        {
            return await _context.TicketsInfo
                .Where(t => t.EventId == request.Id)
                .Select(t => new TicketDto
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId,
                    UserId = t.UserId
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
