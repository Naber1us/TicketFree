using MediatR;

namespace TicketFree.Features.Events.Get
{
    public class GetEventsCommand : IRequest<Event>
    {
        public Guid EventId { get; } = Guid.Empty;
        public Guid OrganizatorId { get; } = Guid.Empty;
        public Guid PlaceId { get; } = Guid.Empty;
        public Guid EventImage { get; } = Guid.Empty;
        public int EventCountTickets { get; } = 0;
        public DateTime EventStart { get; } = DateTime.Now;
        public DateTime EventEnd { get; } = DateTime.Now.AddDays(1);
        public string EventName { get; } = string.Empty;
        public string EventDescription { get; } = string.Empty;
    }
}
