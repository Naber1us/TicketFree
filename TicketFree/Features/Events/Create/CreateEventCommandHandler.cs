using MediatR;
using TicketFree.Interfaces;

namespace TicketFree.Features.Events.Create
{
    public class CreateEventCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateEventCommand, Event>
    {
        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = new Event
            {
                EventId = Guid.NewGuid(),
                EventName = request.EventName,
                EventDescription = request.EventDescription,
                EventImage = request.EventImage,
                EventStart = request.EventStart,
                EventEnd = request.EventEnd,
                PlaceId =   request.PlaceId,
                EventCountTickets = request.EventCountTickets,
                OrganizatorId = request.OrganizatorId,
                EventStatus = request.EventStatus
            };

            dbContext.EventsInfo.Add(@event);
            await dbContext.SaveChangesAsync(cancellationToken);

            return @event;

        }
    }
}
