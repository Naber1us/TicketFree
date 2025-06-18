using TicketFree.Enums;

namespace TicketFree.Features.Events
{
    public class Event
    {
        public Guid EventId { get; set; }
        public Guid OrganizatorId { get; set; }
        public int EventCountTickets { get; set; }
        public int CurrentEventCountTickets { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public EStatus EventStatus { get; set; }
        public required string EventName { get; set; }
        public string EventDescription { get; set; } = string.Empty;
        public Guid EventImage { get; set; }
        public Guid PlaceId { get; set; }
    }
}
