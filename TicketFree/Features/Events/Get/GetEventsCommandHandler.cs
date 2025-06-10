using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events.Dto;
using TicketFree.Interfaces;

namespace TicketFree.Features.Events.Get
{
    public class GetEventByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetEventByIdQuery, EventDto?>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.EventsInfo
                .Where(e => e.EventId == request.Id)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    EventDescription = e.EventDescription,
                    EventImage = e.EventImage,
                    EventStart = e.EventStart,
                    EventEnd = e.EventEnd,
                    PlaceId = e.PlaceId,
                    EventCountTickets = e.EventCountTickets,
                    OrganizatorId = e.OrganizatorId,
                    EventStatus = e.EventStatus
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
    public class GeActiveEventsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetActivesEventsQuery, List<EventDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<EventDto>> Handle(GetActivesEventsQuery request, CancellationToken cancellationToken)
        {
            return await _context.EventsInfo
                .Where(e => e.EventStatus == "Open")
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    EventDescription = e.EventDescription,
                    EventImage = e.EventImage,
                    EventStart = e.EventStart,
                    EventEnd = e.EventEnd,
                    PlaceId = e.PlaceId,
                    EventCountTickets = e.EventCountTickets,
                    OrganizatorId = e.OrganizatorId,
                    EventStatus = e.EventStatus
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
