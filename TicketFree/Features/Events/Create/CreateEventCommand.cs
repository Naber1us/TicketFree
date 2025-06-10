using MediatR;

namespace TicketFree.Features.Events.Create
{
    public class CreateEventCommand : IRequest<Event>
    {
        public Guid OrganizatorId { get; set; } = Guid.Empty;
        public Guid PlaceId { get; set; } = Guid.Empty;
        public Guid EventImage { get; set; } = Guid.Empty;
        public int EventCountTickets { get; set; } = 0;
        public DateTime EventStart { get; set; } = DateTime.Now;
        public DateTime EventEnd { get; set; } = DateTime.Now.AddDays(1);
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string EventStatus { get; set; } = string.Empty;
    }
}
